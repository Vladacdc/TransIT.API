using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using AutoMapper;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Malfunction CRUD service
    /// </summary>
    /// <see cref="IMalfunctionService"/>
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Log on error</param>
        /// <see cref="CrudService{TEntity}"/>
        public RoleService(
            IMapper mapper,
            ILogger<RoleService> logger,
            RoleManager<Role> roleManager)
        {
            this._mapper = mapper;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Asynchronusly creates a new role in a system.
        /// </summary>
        /// <param name="value">The role ENTITY.</param>
        /// <returns>Same entity if operation succeeds or null instead.</returns>
        public async Task<Role> CreateAsync(Role value)
        {
            IdentityResult createResult = await _roleManager.CreateAsync(value);
            return createResult.Succeeded ? value : null;
        }

        public async Task DeleteAsync(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
        }

        public Task<Role> GetAsync(string id)
        {
            return _roleManager.FindByIdAsync(id);
        }

        public Task<Role> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetRangeAsync(uint offset, uint amount)
        {
            return await _roleManager.Roles
                .Skip((int)offset)
                .Take((int)amount)
                .ToListAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> SearchAsync(string search)
        {
            List<string> tokens = search.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToUpperInvariant())
                .ToList();

            List<Role> results = new List<Role>();
            
            foreach (var item in tokens)
            { 

                List<Role> part = await _roleManager.Roles.Where(
                     role => role.NormalizedName == item ||
                             role.TransName == item)
                    .ToListAsync();
                results.AddRange(part);
            }

            return results;
        }

        public async Task<Role> UpdateAsync(Role value)
        {
            Role role = await _roleManager.FindByIdAsync(value.Id);
            role = _mapper.Map(role, value);
            await _roleManager.UpdateAsync(role);
            return role;
        }
    }
}
