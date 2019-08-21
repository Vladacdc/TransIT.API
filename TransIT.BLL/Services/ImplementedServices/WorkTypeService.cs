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
    public class WorkTypeService :IWorkTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public WorkTypeService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<WorkTypeDTO> GetAsync(int id)
        {
            return _mapper.Map<WorkTypeDTO>(await _unitOfWork.WorkTypeRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<WorkTypeDTO>> GetRangeAsync(uint offset, uint amount)
        {
            return _mapper.Map<IEnumerable<WorkTypeDTO>>(
                await _unitOfWork.WorkTypeRepository.GetRangeAsync(offset, amount)
            );
        }

        public async Task<IEnumerable<WorkTypeDTO>> SearchAsync(string search)
        {
            var vehicleTypes = await _unitOfWork.WorkTypeRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
            );

            return _mapper.Map<IEnumerable<WorkTypeDTO>>(await vehicleTypes.ToListAsync());
        }

        public async Task<WorkTypeDTO> CreateAsync(WorkTypeDTO dto)
        {
            var model = _mapper.Map<WorkType>(dto);
            await _unitOfWork.WorkTypeRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<WorkTypeDTO>(model);
        }

        public async Task<WorkTypeDTO> UpdateAsync(WorkTypeDTO dto)
        {
            var model = _mapper.Map<WorkType>(dto);
            _unitOfWork.WorkTypeRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<WorkTypeDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.WorkTypeRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }


    }
}
