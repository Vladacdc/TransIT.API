using System;
using System.Collections.Generic;
using System.Data;
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
    /// <see cref="IActionTypeService"/>
    public class ActionTypeService : IActionTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="mapper"></param>
        public ActionTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionTypeDTO> GetAsync(int id)
        {
            return _mapper.Map<ActionTypeDTO>(await _unitOfWork.ActionTypeRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ActionTypeDTO>> GetRangeAsync(uint offset, uint amount)
        {
            return (await _unitOfWork.ActionTypeRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<ActionTypeDTO>();
        }

        public async Task<IEnumerable<ActionTypeDTO>> SearchAsync(string search)
        {
            var actionTypes = await _unitOfWork.ActionTypeRepository.SearchExpressionAsync(
           search
               .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(x => x.Trim().ToUpperInvariant())
           );

            return actionTypes.ProjectTo<ActionTypeDTO>();
        }
        
        public async Task<ActionTypeDTO> CreateAsync(ActionTypeDTO dto, int? userId = null)
        {
            ActionType model = _mapper.Map<ActionType>(dto);

            if (userId.HasValue)
            {
                model.CreateId = userId;
                model.ModId = userId;
            }

            await _unitOfWork.ActionTypeRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task<ActionTypeDTO> UpdateAsync(ActionTypeDTO dto, int? userId = null)
        {
            ActionType model = _mapper.Map<ActionType>(dto);

            if (model.IsFixed)
            {
                throw new ConstraintException("Current state can not be edited");
            }
            if (dto.IsFixed)
            {
                throw new ArgumentException("Incorrect model");
            }

            if (userId.HasValue)
            {
                model.ModId = userId;
            }

            _unitOfWork.ActionTypeRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            ActionType model = await _unitOfWork.ActionTypeRepository.GetByIdAsync(id);

            if (model.IsFixed)
            {
                throw new ConstraintException("Current state can not be deleted");
            }

            _unitOfWork.ActionTypeRepository.Remove(model);
            await _unitOfWork.SaveAsync();
        }
    }
}