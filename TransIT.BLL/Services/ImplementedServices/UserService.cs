using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.API.Extensions;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// User model CRUD
    /// </summary>
    /// <see cref="IUserService"/>

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public UserService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<UserService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Updates a user in a database.
        /// </summary>
        /// <param name="model">The user ENTITY.</param>
        /// <returns>Same user entity.</returns>
        public async Task<UserDTO> UpdateAsync(UserDTO model)
        {
            try
            {
                User user = await _unitOfWork.UserManager.FindByIdAsync(model.Id);
                IList<string> roles = await _unitOfWork.UserManager.GetRolesAsync(user);
                await _unitOfWork.UserManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
                await _unitOfWork.UserManager.AddToRoleAsync(user, model.Role.Name);
                user = _mapper.Map(model, user);
                await _unitOfWork.UserManager.UpdateAsync(user);
                return _mapper.Map<UserDTO>(user);  
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(UpdateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdateAsync));
                throw e;
            }
        }

        public virtual async Task<UserDTO> UpdatePasswordAsync(UserDTO user, string oldPassword, string newPassword)
        {
            try
            {
                User entity = _mapper.Map<User>(user);
                IdentityResult result = await _unitOfWork.UserManager
                    .ChangePasswordAsync(entity, oldPassword, newPassword);
                return result.Succeeded ? user : null;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(UpdatePasswordAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdatePasswordAsync));
                throw e;
            }
        }

        public virtual async Task<IEnumerable<UserDTO>> GetAssignees(uint offset, uint amount)
        {
            IEnumerable<User> workers = (await _unitOfWork.UserManager.GetUsersInRoleAsync(ROLE.WORKER))
                .Skip((int)offset)
                .Take((int)amount);
            return _mapper.Map<IEnumerable<UserDTO>>(workers);
        }

        public async Task<UserDTO> GetAsync(string id)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.UserManager.FindByIdAsync(id));
        }

        public async Task<IEnumerable<UserDTO>> GetRangeAsync(uint offset, uint amount)
        {
            List<User> source = await _unitOfWork.UserManager.Users
                .Skip((int)offset)
                .Take((int)amount)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(source);
        }

        public async Task<UserDTO> CreateAsync(UserDTO value)
        {
            User user = _mapper.Map<User>(value);
            IdentityResult result = await _unitOfWork.UserManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _unitOfWork.UserManager.AddToRoleAsync(user, value.Role.Name);
            }
            return result.Succeeded ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task DeleteAsync(string id)
        {
            User user = await _unitOfWork.UserManager.FindByIdAsync(id);
            await _unitOfWork.UserManager.DeleteAsync(user);
        }

        public Task<IEnumerable<UserDTO>> SearchAsync(string search)
        {
            throw new NotImplementedException();
        }
    }
}
