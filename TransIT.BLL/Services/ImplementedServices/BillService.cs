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
    /// Bill CRUD service
    /// </summary>
    /// <see cref="IBillService"/>
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public BillService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BillDTO> GetAsync(int id)
        {
            return _mapper.Map<BillDTO>(await _unitOfWork.BillRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<BillDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.BillRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<BillDTO>>(entities);
        }

        public async Task<IEnumerable<BillDTO>> SearchAsync(string search)
        {
            var bills = await _unitOfWork.BillRepository.SearchAsync(
                    new SearchTokenCollection(search)
                );

            return _mapper.Map<IEnumerable<BillDTO>>(await bills.ToListAsync());
        }

        public async Task<BillDTO> CreateAsync(BillDTO dto)
        {
            var model = _mapper.Map<Bill>(dto);

            await _unitOfWork.BillRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<BillDTO>(model);
        }

        public async Task<BillDTO> UpdateAsync(BillDTO dto)
        {
            var model = _mapper.Map<Bill>(dto);

            _unitOfWork.BillRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<BillDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.BillRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}