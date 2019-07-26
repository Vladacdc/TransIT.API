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
    /// Malfunction CRUD service
    /// </summary>
    /// <see cref="IMalfunctionService"/>
    public class MalfunctionService : IMalfunctionService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public MalfunctionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MalfunctionDTO> GetAsync(int id)
        {
            return _mapper.Map<MalfunctionDTO>(await _unitOfWork.MalfunctionRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<MalfunctionDTO>> GetRangeAsync(uint offset, uint amount)
        {
            return (await _unitOfWork.MalfunctionRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<MalfunctionDTO>();
        }

        public async Task<IEnumerable<MalfunctionDTO>> SearchAsync(string search)
        {
            var countries = await _unitOfWork.MalfunctionRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return countries.ProjectTo<MalfunctionDTO>();
        }

        public async Task<MalfunctionDTO> CreateAsync(MalfunctionDTO dto, int? userId = null)
        {
            var model = _mapper.Map<Malfunction>(dto);
            if (userId.HasValue)
            {
                model.CreateId = userId;
                model.ModId = userId;
            }

            await _unitOfWork.MalfunctionRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<MalfunctionDTO> UpdateAsync(MalfunctionDTO dto, int? userId = null)
        {
            var model = _mapper.Map<Malfunction>(dto);
            if (userId.HasValue)
            {
                model.CreateId = userId;
                model.ModId = userId;
            }

            _unitOfWork.MalfunctionRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.MalfunctionRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}