using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Helpers;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Issue CRUD service
    /// </summary>
    /// <see cref="IIssueService"/>
    public class IssueService : CrudService<int, Issue>, IIssueService
    {
        private IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public IssueService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<int, Issue>> logger,
            IIssueRepository repository,
            IVehicleRepository vehicleRepository) : base(unitOfWork, logger, repository)
        {
            _vehicleRepository = vehicleRepository;
        }

        /// <see cref="IIssueService"/>
        public async Task<IEnumerable<Issue>> GetRegisteredIssuesAsync(uint offset, uint amount, string userId)
        {
            var issues = await _repository.GetAllAsync(i => i.CreatedById == userId);
            return issues.AsQueryable().Skip((int)offset).Take((int)amount);
        }

        public override async Task<Issue> CreateAsync(Issue issue)
        {
            Vehicle vehicle = await _vehicleRepository.GetByIdAsync(issue.VehicleId);
            if (IsWarrantyCase(vehicle))
                issue.Warranty = Warranties.WARRANTY_CASE;

            return await base.CreateAsync(issue);
        }

        private bool IsWarrantyCase(Vehicle vehicle)
        {
            return DateTime.Now.CompareTo(vehicle?.WarrantyEndDate) < 0;
        }

        public override async Task<Issue> UpdateAsync(Issue model)
        {
            try
            {
                model = _repository.UpdateWithIgnoreProperty(model, x => x.StateId);
                await _unitOfWork.SaveAsync();
                return model;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(UpdateAsync), e.Entries);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdateAsync));
                throw;
            }
        }

        public async Task DeleteByUserAsync(int issueId, string userId)
        {
            try
            {
                var issueToDelete = await GetAsync(issueId);
                if (issueToDelete?.CreatedById != userId)
                    throw new UnauthorizedAccessException("Current user doesn't have access to delete this issue");

                _repository.Remove(issueToDelete);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var sqlExc = e.GetBaseException() as SqlException;
                if (sqlExc?.Number == 547)
                {
                    _logger.LogDebug(sqlExc, $"Number of sql exception: {sqlExc.Number.ToString()}");
                    throw new ConstraintException("There are constrained entities, delete them firstly.", sqlExc);
                }
                _logger.LogError(e, nameof(DeleteAsync), e.Entries);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(DeleteAsync));
                throw;
            }

        }
    }
}
