using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
    /// Service for Vehicle
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<VehicleDTO> GetAsync(int id)
        {
            return _mapper.Map<VehicleDTO>(await _unitOfWork.VehicleRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<VehicleDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.VehicleRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<VehicleDTO>>(entities);
        }

        public async Task<IEnumerable<VehicleDTO>> SearchAsync(string search)
        {
            var vehicles = await _unitOfWork.VehicleRepository.SearchExpressionAsync(
            search
                .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToUpperInvariant())
            );

            return _mapper.Map<IEnumerable<VehicleDTO>>(await vehicles.ToListAsync());
        }

        public async Task<VehicleDTO> CreateAsync(VehicleDTO value)
        {
            var model = _mapper.Map<Vehicle>(value);
            await _unitOfWork.VehicleRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<VehicleDTO> UpdateAsync(VehicleDTO value)
        {
            var model = _mapper.Map<Vehicle>(value);
            _unitOfWork.VehicleRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return value;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.VehicleRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}