using System;
using System.Collections.Generic;
using System.Data;
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
    public class TransitionService : ITransitionService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public TransitionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransitionDTO> GetAsync(int id)
        {
            return _mapper.Map<TransitionDTO>(await _unitOfWork.TransitionRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<TransitionDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.TransitionRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<TransitionDTO>>(entities);
        }

        public async Task<IEnumerable<TransitionDTO>> SearchAsync(string search)
        {
            var transitions = await _unitOfWork.TransitionRepository.SearchExpressionAsync(
                            search
                                .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim().ToUpperInvariant())
                            );

            return _mapper.Map<IEnumerable<TransitionDTO>>(await transitions.ToListAsync());
        }

        public async Task<TransitionDTO> CreateAsync(TransitionDTO dto)
        {
            var model = _mapper.Map<Transition>(dto);
            await _unitOfWork.TransitionRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TransitionDTO>(model);
        }

        public async Task<TransitionDTO> UpdateAsync(TransitionDTO dto)
        {
            var model = _mapper.Map<Transition>(dto);
            if (model.IsFixed)
            {
                throw new ConstraintException("Can not be edited");
            }
            if (dto.IsFixed)
            {
                throw new ArgumentException("Incorrect model");
            }

            _unitOfWork.TransitionRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TransitionDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            var model = await GetAsync(id);
            if (model.IsFixed)
            {
                throw new ConstraintException("Can not be deleted");
            }

            _unitOfWork.TransitionRepository.Remove(model);
            await _unitOfWork.SaveAsync();
        }
    }
}