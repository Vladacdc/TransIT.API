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
    /// <summary>
    /// IssueLog CRUD service
    /// </summary>
    /// <see cref="IIssueLogService"/>
    public class IssueLogService : IIssueLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public IssueLogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IssueLogDTO> GetAsync(int id)
        {
            return _mapper.Map<IssueLogDTO>(await _unitOfWork.IssueLogRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<IssueLogDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.IssueLogRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<IssueLogDTO>>(entities);
        }

        public async Task<IEnumerable<IssueLogDTO>> GetRangeByIssueIdAsync(int issueId)
        {
            var entities = await _unitOfWork.IssueLogRepository.GetAllAsync(i => i.IssueId == issueId);
            return _mapper.Map<IEnumerable<IssueLogDTO>>(entities);
        }

        public async Task<IEnumerable<IssueLogDTO>> SearchAsync(string search)
        {
            var issueLogs = await _unitOfWork.IssueLogRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
            );

            return _mapper.Map<IEnumerable<IssueLogDTO>>(await issueLogs.ToListAsync());
        }

        public async Task<IssueLogDTO> UpdateAsync(IssueLogDTO dto)
        {
            var model = _mapper.Map<IssueLog>(dto);

            var newDto = _mapper.Map<IssueLogDTO>(_unitOfWork.IssueLogRepository.Update(model));
            await _unitOfWork.SaveAsync();
            return newDto;
        }

        public async Task<IssueLogDTO> CreateAsync(IssueLogDTO issueLogDTO)
        {
            var oldIssueDTO = issueLogDTO.Issue;
            issueLogDTO.Issue =
                _mapper.Map<IssueDTO>(await _unitOfWork.IssueRepository.GetByIdAsync((int)issueLogDTO.Issue.Id));
            issueLogDTO.OldState.Id = issueLogDTO.Issue.State.Id;
            issueLogDTO.Issue.State.Id = issueLogDTO.NewState.Id;
            issueLogDTO.Issue.Deadline = oldIssueDTO.Deadline;
            issueLogDTO.Issue.AssignedTo.Id = oldIssueDTO.AssignedTo.Id;

            if (issueLogDTO.OldState.Id != issueLogDTO.NewState.Id
                && !(await _unitOfWork.TransitionRepository.GetAllAsync(x =>
                        x.FromStateId == issueLogDTO.OldState.Id
                        && x.ActionTypeId == issueLogDTO.ActionType.Id
                        && x.ToStateId == issueLogDTO.NewState.Id)
                    ).Any())
                throw new ConstraintException("Can not move to the state according to transition settings.");

            var model = _mapper.Map<IssueLog>(issueLogDTO);

            await _unitOfWork.IssueLogRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.IssueLogRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}