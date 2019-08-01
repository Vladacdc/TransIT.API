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
            var entities = await _unitOfWork.MalfunctionRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<MalfunctionDTO>>(entities);
        }

        public async Task<IEnumerable<MalfunctionDTO>> SearchAsync(string search)
        {
            var malfunctions = await _unitOfWork.MalfunctionRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return _mapper.Map<IEnumerable<MalfunctionDTO>>(await malfunctions.ToListAsync());
        }

        public async Task<MalfunctionDTO> CreateAsync(MalfunctionDTO dto)
        {
            var model = _mapper.Map<Malfunction>(dto);

            await _unitOfWork.MalfunctionRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MalfunctionDTO>(model);
        }

        public async Task<MalfunctionDTO> UpdateAsync(MalfunctionDTO dto)
        {
            var model = _mapper.Map<Malfunction>(dto);

            _unitOfWork.MalfunctionRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MalfunctionDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.MalfunctionRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}