using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    public class PartsInService : IPartsInService
    {
        private readonly IPartsInRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartsInService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.PartsInRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PartInDTO> CreateAsync(PartInDTO dto)
        {
            var entity = await _repository.AddAsync(_mapper.Map<PartIn>(dto));
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PartInDTO>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PartInDTO> GetAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<PartInDTO>(entity);
        }

        public async Task<IEnumerable<PartInDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _repository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<PartInDTO>>(entities);
        }

        public async Task<IEnumerable<PartInDTO>> SearchAsync(string search)
        {
            var issues = await _unitOfWork.IssueRepository.SearchAsync(
                new SearchTokenCollection(search)
            );
            return _mapper.Map<IEnumerable<PartInDTO>>(await issues.ToListAsync());
        }

        public async Task<PartInDTO> UpdateAsync(PartInDTO dto)
        {
            var entity = _mapper.Map<PartIn>(dto);
            _repository.Update(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PartInDTO>(entity);
        }
    }
}
