using System;
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
            var employees = await _unitOfWork.EmployeeRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return _mapper.Map<IEnumerable<EmployeeDTO>>(await employees.ToListAsync());
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO dto)
        {
            var model = _mapper.Map<Employee>(dto);

            await _unitOfWork.EmployeeRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task<EmployeeDTO> UpdateAsync(EmployeeDTO dto)
        {
            var model = _mapper.Map<Employee>(dto);

            _unitOfWork.EmployeeRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.EmployeeRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}