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
    /// Currency CRUD service
    /// </summary>
    /// <see cref="ICurrencyService"/>
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CurrencyDTO> GetAsync(int id)
        {
            return _mapper.Map<CurrencyDTO>(await _unitOfWork.CurrencyRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<CurrencyDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.CurrencyRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<CurrencyDTO>>(entities);
        }

        public async Task<IEnumerable<CurrencyDTO>> SearchAsync(string search)
        {
            var currencies = await _unitOfWork.CurrencyRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return _mapper.Map<IEnumerable<CurrencyDTO>>(await currencies.ToListAsync());
        }

        public async Task<CurrencyDTO> CreateAsync(CurrencyDTO dto)
        {
            var model = _mapper.Map<Currency>(dto);

            await _unitOfWork.CurrencyRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task<CurrencyDTO> UpdateAsync(CurrencyDTO dto)
        {
            var model = _mapper.Map<Currency>(dto);

            _unitOfWork.CurrencyRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.CurrencyRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}