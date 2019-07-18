using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper)
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
            return (await _unitOfWork.EmployeeRepository.GetRangeAsync(offset, amount)).AsQueryable().ProjectTo<EmployeeDTO>();
        }

        public async Task<IEnumerable<EmployeeDTO>> SearchAsync(string search)
        {
            var countries = await _unitOfWork.EmployeeRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return countries.ProjectTo<EmployeeDTO>();
        }


        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO dto)
        {
            var model = _mapper.Map<Employee>(dto);
            await _unitOfWork.EmployeeRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
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
