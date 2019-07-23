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
            return (await _unitOfWork.VehicleRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<VehicleDTO>();
        }

        public async Task<IEnumerable<VehicleDTO>> SearchAsync(string search)
        {
            var vehicles = await _unitOfWork.VehicleRepository.SearchExpressionAsync(
            search
                .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToUpperInvariant())
            );

            return vehicles.ProjectTo<VehicleDTO>();
        }

        public async Task<VehicleDTO> CreateAsync(VehicleDTO value)
        {
            Vehicle model = _mapper.Map<Vehicle>(value);
            await _unitOfWork.VehicleRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<VehicleDTO> UpdateAsync(VehicleDTO value)
        {
            Vehicle model = _mapper.Map<Vehicle>(value);
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