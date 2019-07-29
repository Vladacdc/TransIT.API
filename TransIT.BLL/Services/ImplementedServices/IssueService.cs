using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TransIT.BLL.DTOs;
using TransIT.BLL.Helpers;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Issue CRUD service
    /// </summary>
    /// <see cref="IIssueService"/>
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public IssueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IssueDTO> GetAsync(int id)
        {
            return _mapper.Map<IssueDTO>(await _unitOfWork.IssueRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<IssueDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.IssueRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<IssueDTO>>(entities);
        }

        /// <see cref="IIssueService"/>
        public async Task<IEnumerable<IssueDTO>> GetRegisteredIssuesAsync(uint offset, uint amount, string userId)
        {
            var entities = await _unitOfWork.IssueRepository.GetQueryable()
                  .Where(i => i.CreatedById == userId)
                  .Skip((int)offset)
                  .Take((int)amount)
                  .ToListAsync();
            return _mapper.Map<IEnumerable<IssueDTO>>(entities);
        }

        public async Task<IEnumerable<IssueDTO>> SearchAsync(string search)
        {
            var issues = await _unitOfWork.IssueRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return _mapper.Map<IEnumerable<IssueDTO>>(await issues.ToListAsync());
        }

        public async Task<IssueDTO> CreateAsync(IssueDTO dto)
        {
            var vehicle = _mapper.Map<VehicleDTO>(await _unitOfWork.VehicleRepository.GetByIdAsync(dto.Vehicle.Id));
            if (IsWarrantyCase(vehicle))
            {
                dto.Warranty = Warranties.WARRANTY_CASE;
            }

            var model = _mapper.Map<Issue>(dto);

            await _unitOfWork.IssueRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        private bool IsWarrantyCase(VehicleDTO vehicle)
        {
            return DateTime.Now.CompareTo(vehicle?.WarrantyEndDate) < 0;
        }

        public async Task<IssueDTO> UpdateAsync(IssueDTO dto)
        {
            var model = _mapper.Map<Issue>(dto);

            dto = _mapper.Map<IssueDTO>(_unitOfWork.IssueRepository.UpdateWithIgnoreProperty(model, x => x.StateId));
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteByUserAsync(int issueId, string userId)
        {
            var issueToDelete = await GetAsync(issueId);
            if (issueToDelete?.CreatedById != userId)
            {
                throw new UnauthorizedAccessException("Current user doesn't have access to delete this issue");
            }

            _unitOfWork.IssueRepository.Remove(issueToDelete.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.IssueRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ulong> GetTotalRecordsForSpecificUser(string userId)
        {
            return (ulong)await _unitOfWork.IssueRepository.GetQueryable()
                .Where(issue => issue.CreatedById == userId)
                .LongCountAsync();
        }

        public async Task<IEnumerable<IssueDTO>> GetIssuesBySpecificUser(string userId)
        {
            var entities = await _unitOfWork.IssueRepository.GetQueryable()
                .Where(issue => issue.CreatedById == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<IssueDTO>>(entities);
        }
    }
}