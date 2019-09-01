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
            var issueLogs = await _unitOfWork.IssueLogRepository.SearchAsync(
                new SearchTokenCollection(search)
            );

            return _mapper.Map<IEnumerable<IssueLogDTO>>(await issueLogs.ToListAsync());
        }

        public async Task<IssueLogDTO> UpdateAsync(IssueLogDTO dto)
        {
            var model = _mapper.Map<IssueLog>(dto);

            _unitOfWork.IssueLogRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<IssueLogDTO>(model);
        }

        public async Task<IssueLogDTO> CreateAsync(IssueLogDTO issueLogDTO)
        {
            //load previous state of issue and change AssignedTo, State, Deadline fields
            var oldIssueDTO = issueLogDTO.Issue;
            issueLogDTO.Issue =
                _mapper.Map<IssueDTO>(await _unitOfWork.IssueRepository.GetByIdAsync((int)issueLogDTO.Issue.Id));
            issueLogDTO.OldState = issueLogDTO.Issue.State;

            if (issueLogDTO.OldState != issueLogDTO.NewState
                && !(await _unitOfWork.TransitionRepository.GetAllAsync(x =>
                        x.FromStateId == issueLogDTO.OldState.Id
                        && x.ActionTypeId == issueLogDTO.ActionType.Id
                        && x.ToStateId == issueLogDTO.NewState.Id)
                    ).Any())
                throw new ConstraintException("Cannot change state according to the transition settings.");

            var model = _mapper.Map<IssueLog>(issueLogDTO);

            await _unitOfWork.IssueLogRepository.AddAsync(model);
            //changed this fields in models, not DTO because GetByIdAsync and mapping doesn't allow saving changes
            model.Issue.StateId = model.NewStateId;
            model.Issue.AssignedToId = oldIssueDTO.AssignedTo.Id;
            model.Issue.Deadline = oldIssueDTO.Deadline;
            await _unitOfWork.SaveAsync();
            return _mapper.Map<IssueLogDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.IssueLogRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}