using System;
using System.Collections.Generic;
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
            return (await _unitOfWork.LocationRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<LocationDTO>();
        }

        public async Task<IEnumerable<LocationDTO>> SearchAsync(string search)
        {
            var locations = await _unitOfWork.LocationRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return locations.ProjectTo<LocationDTO>();
        }

        public async Task<LocationDTO> CreateAsync(LocationDTO dto)
        {
            var model = _mapper.Map<Location>(dto);
            await _unitOfWork.LocationRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<LocationDTO> UpdateAsync(LocationDTO dto)
        {
            var model = _mapper.Map<Location>(dto);
            _unitOfWork.LocationRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.LocationRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}