using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            return (await _unitOfWork.SupplierRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<SupplierDTO>();
        }

        public async Task<IEnumerable<SupplierDTO>> SearchAsync(string search)
        {
            var suppliers = await _unitOfWork.SupplierRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return suppliers.ProjectTo<SupplierDTO>();
        }

        public async Task<SupplierDTO> CreateAsync(SupplierDTO dto)
        {
            var model = _mapper.Map<Supplier>(dto);
            await _unitOfWork.SupplierRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<SupplierDTO> CreateAsync(int userId,SupplierDTO dto)
        {
            Supplier model = _mapper.Map<Supplier>(dto);

            model.CreateId = userId;
            model.ModId = userId;

            await _unitOfWork.SupplierRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<SupplierDTO> UpdateAsync(SupplierDTO dto)
        {
            var model = _mapper.Map<Supplier>(dto);
            _unitOfWork.SupplierRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task<SupplierDTO> UpdateAsync(int userId, SupplierDTO stateDto)
        {
            Supplier model = _mapper.Map<Supplier>(stateDto);

            model.ModId = userId;

            _unitOfWork.SupplierRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return stateDto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.SupplierRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}