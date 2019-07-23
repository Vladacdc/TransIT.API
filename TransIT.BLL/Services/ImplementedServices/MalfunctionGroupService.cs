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
    /// Malfunction Group CRUD service
    /// </summary>
    /// <see cref="IMalfunctionGroupService"/>
    public class MalfunctionGroupService : IMalfunctionGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public MalfunctionGroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MalfunctionGroupDTO> GetAsync(int id)
        {
            return _mapper.Map<MalfunctionGroupDTO>(await _unitOfWork.MalfunctionGroupRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<MalfunctionGroupDTO>> GetRangeAsync(uint offset, uint amount)
        {
            return (await _unitOfWork.MalfunctionGroupRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<MalfunctionGroupDTO>();
        }

        public async Task<IEnumerable<MalfunctionGroupDTO>> SearchAsync(string search)
        {
            var malfunctionGroupsDTO = await _unitOfWork.MalfunctionGroupRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return malfunctionGroupsDTO.ProjectTo<MalfunctionGroupDTO>();
        }

        public async Task<MalfunctionGroupDTO> CreateAsync(MalfunctionGroupDTO dto)
        {
            var model = _mapper.Map<MalfunctionGroup>(dto);
            await _unitOfWork.MalfunctionGroupRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<MalfunctionGroupDTO> UpdateAsync(MalfunctionGroupDTO dto)
        {
            var model = _mapper.Map<MalfunctionGroup>(dto);
            _unitOfWork.MalfunctionGroupRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.MalfunctionGroupRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}