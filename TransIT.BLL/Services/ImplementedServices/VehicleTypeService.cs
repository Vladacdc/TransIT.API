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
    /// Service for Vehicle Type
    /// </summary>
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public VehicleTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<VehicleTypeDTO> GetAsync(int id)
        {
            return _mapper.Map<VehicleTypeDTO>(await _unitOfWork.VehicleTypeRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<VehicleTypeDTO>> GetRangeAsync(uint offset, uint amount)
        {
            return _mapper.Map<IEnumerable<VehicleTypeDTO>>(
                await _unitOfWork.VehicleTypeRepository.GetRangeAsync(offset, amount)
            );
        }

        public async Task<IEnumerable<VehicleTypeDTO>> SearchAsync(string search)
        {
            var vehicleTypes = await _unitOfWork.VehicleTypeRepository.SearchExpressionAsync(
            search
                .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToUpperInvariant())
            );

            return _mapper.Map<IEnumerable<VehicleTypeDTO>>(await vehicleTypes.ToListAsync());
        }

        public async Task<VehicleTypeDTO> CreateAsync(VehicleTypeDTO vehicleTypeDto)
        {
            var model = _mapper.Map<VehicleType>(vehicleTypeDto);
            await _unitOfWork.VehicleTypeRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<VehicleTypeDTO> UpdateAsync(VehicleTypeDTO vehicleTypeDto)
        {
            var model = _mapper.Map<VehicleType>(vehicleTypeDto);
            _unitOfWork.VehicleTypeRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return vehicleTypeDto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.VehicleTypeRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}