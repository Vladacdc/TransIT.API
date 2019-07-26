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
        private static RoleManager<Role> _roleManager;
        private static UserManager<User> _userManager;

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
            ILogger<UserService> logger,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
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
                User user = await _userManager.FindByIdAsync(model.Id);
                IList<string> roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
                await _userManager.AddToRoleAsync(user, model.Role.Name);
                user = _mapper.Map(model, user);
                await _userManager.UpdateAsync(user);
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
                IdentityResult result = await _userManager
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
            IEnumerable<User> workers = (await _userManager.GetUsersInRoleAsync(ROLE.WORKER))
                .Skip((int)offset)
                .Take((int)amount);
            return _mapper.Map<IEnumerable<UserDTO>>(workers);
        }

        public async Task<UserDTO> GetAsync(string id)
        {
            return _mapper.Map<UserDTO>(await _userManager.FindByIdAsync(id));
        }

        public async Task<IEnumerable<UserDTO>> GetRangeAsync(uint offset, uint amount)
        {
            List<User> source = await _userManager.Users
                .Skip((int)offset)
                .Take((int)amount)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(source);
        }

        public async Task<UserDTO> CreateAsync(UserDTO value)
        {
            User user = _mapper.Map<User>(value);
            IdentityResult result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, value.Role.Name);
            }
            return result.Succeeded ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task DeleteAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public Task<IEnumerable<UserDTO>> SearchAsync(string search)
        {
            throw new NotImplementedException();
        }
    }
}
