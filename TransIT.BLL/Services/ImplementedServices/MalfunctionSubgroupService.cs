﻿using System;
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
    /// Malfunction Subgroup CRUD service
    /// </summary>
    /// <see cref="IMalfunctionSubgroupService"/>
    public class MalfunctionSubgroupService : IMalfunctionSubgroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public MalfunctionSubgroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MalfunctionSubgroupDTO> GetAsync(int id)
        {
            return _mapper.Map<MalfunctionSubgroupDTO>(await _unitOfWork.MalfunctionSubgroupRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<MalfunctionSubgroupDTO>> GetRangeAsync(uint offset, uint amount)
        {
            return (await _unitOfWork.MalfunctionSubgroupRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<MalfunctionSubgroupDTO>();
        }

        public async Task<IEnumerable<MalfunctionSubgroupDTO>> SearchAsync(string search)
        {
            var countries = await _unitOfWork.MalfunctionSubgroupRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return countries.ProjectTo<MalfunctionSubgroupDTO>();
        }

        public async Task<MalfunctionSubgroupDTO> CreateAsync(MalfunctionSubgroupDTO dto, int? userId = null)
        {
            var model = _mapper.Map<MalfunctionSubgroup>(dto);
            if(userId.HasValue)
            {
                model.CreateId = userId;
                model.ModId = userId;
            }

            await _unitOfWork.MalfunctionSubgroupRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<MalfunctionSubgroupDTO> UpdateAsync(MalfunctionSubgroupDTO dto, int? userId = null)
        {
            var model = _mapper.Map<MalfunctionSubgroup>(dto);
            if(userId.HasValue)
            {
                model.CreateId = userId;
                model.ModId = userId;
            }

            _unitOfWork.MalfunctionSubgroupRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.MalfunctionSubgroupRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}