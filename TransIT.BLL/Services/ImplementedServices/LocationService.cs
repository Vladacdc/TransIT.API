using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LocationDTO> GetAsync(int id)
        {
            return _mapper.Map<LocationDTO>(await _unitOfWork.LocationRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<LocationDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.LocationRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<LocationDTO>>(entities);
        }

        public async Task<IEnumerable<LocationDTO>> SearchAsync(string search)
        {
            var locations = await _unitOfWork.LocationRepository.SearchAsync(
                    new SearchTokenCollection(search)
                );

            return _mapper.Map<IEnumerable<LocationDTO>>(await locations.ToListAsync());
        }

        public async Task<LocationDTO> CreateAsync(LocationDTO dto)
        {
            var model = _mapper.Map<Location>(dto);
            await _unitOfWork.LocationRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<LocationDTO>(model);
        }

        public async Task<LocationDTO> UpdateAsync(LocationDTO dto)
        {
            var model = _mapper.Map<Location>(dto);
            _unitOfWork.LocationRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<LocationDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.LocationRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}