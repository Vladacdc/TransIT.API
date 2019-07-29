using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Country CRUD service
    /// </summary>
    /// <see cref="ICountryService"/>
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="mapper"></param>
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CountryDTO> GetAsync(int id)
        {
            return _mapper.Map<CountryDTO>(await _unitOfWork.CountryRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<CountryDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.CountryRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<CountryDTO>>(entities);
        }

        public async Task<IEnumerable<CountryDTO>> SearchAsync(string search)
        {
            var countries = await _unitOfWork.CountryRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return _mapper.Map<IEnumerable<CountryDTO>>(await countries.ToListAsync());
        }

        public async Task<CountryDTO> CreateAsync(CountryDTO dto)
        {
            var model = _mapper.Map<Country>(dto);

            await _unitOfWork.CountryRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task<CountryDTO> UpdateAsync(CountryDTO dto)
        {
            var model = _mapper.Map<Country>(dto);

            _unitOfWork.CountryRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.CountryRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}