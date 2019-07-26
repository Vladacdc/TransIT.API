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
            return (await _unitOfWork.BillRepository.GetRangeAsync(offset, amount)).AsQueryable().ProjectTo<BillDTO>();
        }

        public async Task<IEnumerable<BillDTO>> SearchAsync(string search)
        {
            var bills = await _unitOfWork.BillRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return bills.ProjectTo<BillDTO>();
        }

        public async Task<BillDTO> CreateAsync(BillDTO dto, int? userId = null)
        {
            Bill model = _mapper.Map<Bill>(dto);

            if (userId.HasValue)
            {
                model.CreateId = userId;
                model.ModId = userId;
            }

            await _unitOfWork.BillRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task<BillDTO> UpdateAsync(BillDTO dto, int? userId = null)
        {
            var model = _mapper.Map<Bill>(dto);

            if (userId.HasValue)
            {
                model.ModId = userId;
            }

            _unitOfWork.BillRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.BillRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}