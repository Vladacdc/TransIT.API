using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.API.Extensions;
using TransIT.BLL.Security.Hashers;
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

    public class UserService : CrudService<User>, IUserService
    {
        /// <summary>
        /// Manages password hashing
        /// </summary>
        protected IPasswordHasher _hasher;

        protected IRoleRepository _roleRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public UserService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<User>> logger,
            IUserRepository repository,
            IRoleRepository roleRepository,
            IPasswordHasher hasher) : base(unitOfWork, logger, repository)
        {
            _hasher = hasher;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Creates user if login and password not empty and does not exist in DB
        /// hashes password and set zero to id
        /// </summary>
        /// <see cref="IPasswordHasher.HashPassword(string)"/>
        /// <param name="user">User model</param>
        /// <returns>Is successful</returns>
        public override async Task<User> CreateAsync(User user)
        {
            user.Password = _hasher.HashPassword(user.Password);
            return await base.CreateAsync(user);
        }

        public override async Task<User> UpdateAsync(User model)
        {
            try
            {
                var res = _repository.UpdateWithIgnoreProperty(model, u => u.Password);
                await _unitOfWork.SaveAsync(); 
                return res;
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

        public virtual async Task<User> UpdatePasswordAsync(User user, string newPassword)
        {
            
            try
            {
                user.Password = _hasher.HashPassword(newPassword);

                var res = _repository.Update(user);
                await _unitOfWork.SaveAsync();
                return res;
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

        public virtual async Task<IEnumerable<User>> GetAssignees(uint offset, uint amount) =>
            (await _repository.GetAllAsync())
            .AsQueryable()
            .Where(x => x.Role.Name == ROLE.WORKER)
            .Skip((int)offset)
            .Take((int)amount);
    }
}
