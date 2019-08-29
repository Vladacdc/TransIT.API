using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Employee CRUD service
    /// </summary>
    /// <see cref="IEmployeeService"/>
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> GetAsync(int id)
        {
            return _mapper.Map<EmployeeDTO>(await _unitOfWork.EmployeeRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<EmployeeDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.EmployeeRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<EmployeeDTO>>(entities);
        }

        public async Task<IEnumerable<EmployeeDTO>> SearchAsync(string search)
        {
            var employees = await _unitOfWork.EmployeeRepository.SearchAsync(
                    new SearchTokenCollection(search)
                );

            return _mapper.Map<IEnumerable<EmployeeDTO>>(await employees.ToListAsync());
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO dto)
        {
            var model = _mapper.Map<Employee>(dto);

            await _unitOfWork.EmployeeRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<EmployeeDTO>(model);
        }

        public async Task<EmployeeDTO> UpdateAsync(EmployeeDTO dto)
        {
            var model = _mapper.Map<Employee>(dto);

            _unitOfWork.EmployeeRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<EmployeeDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.EmployeeRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Attaches an existing user to employee.
        /// </summary>
        /// <param name="employee">An employee ID.</param>
        /// <param name="user">A user ID.</param>
        /// <returns>A data tranfer object, with attached user.</returns>
        public async Task<EmployeeDTO> AttachUserAsync(int employee, string user)
        {
            var userEntity = await _unitOfWork.UserManager.FindByIdAsync(user);
            var employeeEntity = await _unitOfWork.EmployeeRepository.GetByIdAsync(employee);
 
            if (userEntity != null && employeeEntity != null)
            {
                bool wasAttached = await _unitOfWork.EmployeeRepository
                    .GetQueryable()
                    .AnyAsync(e => e.AttachedUserId == user);

                if (!wasAttached)
                { 
                    employeeEntity.AttachedUserId = userEntity.Id;
                    await _unitOfWork.SaveAsync();
                    employeeEntity.AttachedUser = userEntity;
                    return _mapper.Map<EmployeeDTO>(employeeEntity);
                }
            }
            return null;
        }

        /// <summary>
        /// Removes an attached user from employee account.
        /// </summary>
        /// <param name="employee">An employee ID.</param>
        /// <returns>A data tranfer object, without attached user.</returns>
        public async Task<EmployeeDTO> RemoveUserAsync(int employee)
        {
            var employeeEntity = await _unitOfWork.EmployeeRepository.GetByIdAsync(employee);

            if (employeeEntity != null)
            {
                employeeEntity.AttachedUserId = null;
                employeeEntity.AttachedUser = null;
                await _unitOfWork.SaveAsync();
                return _mapper.Map<EmployeeDTO>(employeeEntity);
            }
            return null;
        }

        /// <summary>
        /// Gets an employee, which has a given user attached to it.
        /// </summary>
        /// <param name="user">A user ID.</param>
        /// <returns>A data tranfer object, with attached user.</returns>
        public async Task<EmployeeDTO> GetEmployeeForUserAsync(string user)
        {
            var employeeEntity = await _unitOfWork.EmployeeRepository.GetQueryable()
                .Where(e => e.AttachedUserId == user)
                .SingleOrDefaultAsync();
            return _mapper.Map<EmployeeDTO>(employeeEntity);
        }

        /// <summary>
        /// Gets a list of users, which were not attached to
        /// an employee, though can be attached.
        /// </summary>
        /// <returns>A list of <see cref="UserDTO"/>.</returns>
        public async Task<List<UserDTO>> GetNotAttachedUsersAsync()
        {
            var entities = await _unitOfWork.UserManager.Users
                 .Where(u => u.Employees.Count == 0)
                 .ToListAsync();
            return _mapper.Map<List<UserDTO>>(entities);
        }

        /// <summary>
        /// Gets all board numbers at once.
        /// </summary>
        /// <returns>A list of integers.</returns>
        public Task<List<int>> GetBoardNumbersAsync()
        {
            return _unitOfWork.EmployeeRepository
                .GetQueryable()
                .Where(e => e.AttachedUserId == null)
                .Select(e => e.BoardNumber)
                .OrderBy(n => n)
                .ToListAsync();
        }

        /// <summary>
        /// Gets an employee by its board number.
        /// </summary>
        /// <param name="boardNumber">A board number.</param>
        /// <returns>A</returns>
        public async Task<EmployeeDTO> GetByBoardNumberAsync(int boardNumber)
        {
            var entity = await _unitOfWork.EmployeeRepository
                .GetQueryable()
                .Where(e => e.BoardNumber == boardNumber)
                .SingleOrDefaultAsync();
            return _mapper.Map<EmployeeDTO>(entity);
        }
    }
}