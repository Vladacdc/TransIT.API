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

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public UserService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Updates a user in a database.
        /// </summary>
        /// <param name="model">The user DTO.</param>
        /// <returns>Updated user DTO.</returns>
        public async Task<UserDTO> UpdateAsync(UserDTO model)
        {
            User user = await _unitOfWork.UserManager.FindByIdAsync(model.Id);
            IList<string> roles = await _unitOfWork.UserManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                await _unitOfWork.UserManager.RemoveFromRoleAsync(user, roles.First());
            }

            await _unitOfWork.UserManager.AddToRoleAsync(user, model.Role.Name);
            user = _mapper.Map(model, user);
            IdentityResult updateResult = await _unitOfWork.UserManager.UpdateAsync(user);
            return updateResult.Succeeded ? await GetAsync(user) : null;
        }

        public virtual async Task<UserDTO> UpdatePasswordAsync(UserDTO user, string oldPassword, string newPassword)
        {
            User entity = _mapper.Map<User>(user);
            IdentityResult result = await _unitOfWork.UserManager
                .ChangePasswordAsync(entity, oldPassword, newPassword);
            return result.Succeeded ? user : null;
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
            User entity = await _unitOfWork.UserManager.FindByIdAsync(id);
            return entity == null ? null : await GetAsync(entity);
        }

        public async Task<IEnumerable<UserDTO>> GetRangeAsync(uint offset, uint amount)
        {
            List<User> source = await _unitOfWork.UserManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
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

            return result.Succeeded ? await GetAsync(user) : null;
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

        private async Task<UserDTO> GetAsync(User entity)
        {
            IList<string> roles = await _unitOfWork.UserManager.GetRolesAsync(entity);
            UserDTO userDTO = _mapper.Map<UserDTO>(entity);
            if (roles.Count > 0)
            {
                Role roleEntity = await _unitOfWork.RoleManager.FindByNameAsync(roles.First());
                userDTO.Role = _mapper.Map<RoleDTO>(roleEntity);
            }

            return userDTO;
        }
    }
}
