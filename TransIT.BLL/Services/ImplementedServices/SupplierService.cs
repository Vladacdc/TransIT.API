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
    /// Supplier CRUD service
    /// </summary>
    /// <see cref="ISupplierService"/>
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SupplierDTO> GetAsync(int id)
        {
            return _mapper.Map<SupplierDTO>(await _unitOfWork.SupplierRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<SupplierDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.SupplierRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<SupplierDTO>>(entities);
        }

        public async Task<IEnumerable<SupplierDTO>> SearchAsync(string search)
        {
            var suppliers = await _unitOfWork.SupplierRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return _mapper.Map<IEnumerable<SupplierDTO>>(await suppliers.ToListAsync());
        }

        public async Task<SupplierDTO> CreateAsync(SupplierDTO supplierDto)
        {
            var model = _mapper.Map<Supplier>(supplierDto);
            await _unitOfWork.SupplierRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<SupplierDTO> UpdateAsync(SupplierDTO supplierDto)
        {
            var model = _mapper.Map<Supplier>(supplierDto);
            _unitOfWork.SupplierRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return supplierDto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.SupplierRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}