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
            var entities = await _unitOfWork.MalfunctionSubgroupRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<MalfunctionSubgroupDTO>>(entities);
        }

        public async Task<IEnumerable<MalfunctionSubgroupDTO>> GetByGroupNameAsync(string groupName)
        {
            var entities = await _unitOfWork.MalfunctionSubgroupRepository.GetAllAsync(
                ms => ms.MalfunctionGroup.Name == groupName);

            return _mapper.Map<IEnumerable<MalfunctionSubgroupDTO>>(entities);
        }

        public async Task<IEnumerable<MalfunctionSubgroupDTO>> SearchAsync(string search)
        {
            var countries = await _unitOfWork.MalfunctionSubgroupRepository.SearchAsync(
                    new SearchTokenCollection(search)
                );

            return _mapper.Map<IEnumerable<MalfunctionSubgroupDTO>>(await countries.ToListAsync());
        }

        public async Task<MalfunctionSubgroupDTO> CreateAsync(MalfunctionSubgroupDTO dto)
        {
            var model = _mapper.Map<MalfunctionSubgroup>(dto);
            await _unitOfWork.MalfunctionSubgroupRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MalfunctionSubgroupDTO>(model);
        }

        public async Task<MalfunctionSubgroupDTO> UpdateAsync(MalfunctionSubgroupDTO dto)
        {
            var model = _mapper.Map<MalfunctionSubgroup>(dto);
            _unitOfWork.MalfunctionSubgroupRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MalfunctionSubgroupDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.MalfunctionSubgroupRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}