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
            return (await _unitOfWork.VehicleTypeRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<VehicleTypeDTO>();
        }

        public async Task<IEnumerable<VehicleTypeDTO>> SearchAsync(string search)
        {
            var vehicleTypes = await _unitOfWork.VehicleTypeRepository.SearchExpressionAsync(
            search
                .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToUpperInvariant())
            );

            return vehicleTypes.ProjectTo<VehicleTypeDTO>();
        }

        public async Task<VehicleTypeDTO> CreateAsync(VehicleTypeDTO vehicleTypeDto, int? userId = null)
        {
            var model = _mapper.Map<VehicleType>(vehicleTypeDto);
            if (userId != null)
            {
                model.CreateId = userId;
                model.ModId = userId;
            }
            await _unitOfWork.VehicleTypeRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<VehicleTypeDTO> UpdateAsync(VehicleTypeDTO vehicleTypeDto, int? userId = null)
        {
            var model = _mapper.Map<VehicleType>(vehicleTypeDto);
            if (userId != null)
            {
                model.ModId = userId;
            }
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