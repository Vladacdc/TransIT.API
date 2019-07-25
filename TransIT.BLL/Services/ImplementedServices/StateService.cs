using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// State Group CRUD service
    /// </summary>
    /// <see cref="IStateService"/>
    public class StateService : CrudService<State>, IStateService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public StateService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<State>> logger,
            IStateRepository repository) : base(unitOfWork, logger, repository) { }

        /// <summary>
        /// Returns state by name
        /// </summary>
        /// <param name="name">State's name</param>
        /// <returns>State</returns>
        /// <see cref="IStateService"/>
        public async Task<State> GetStateByNameAsync(string name)
        {
            var states = await _repository.GetAllAsync(s => s.Name == name);
            return states.SingleOrDefault();
        }

        public async override Task<State> UpdateAsync(State model)
        {
            try
            {
                var newModel = await GetAsync(model.Id);
                if (newModel.IsFixed)
                {
                    throw new ConstraintException("Current state can not be edited");
                }
                if(model.IsFixed)
                {
                    throw new ArgumentException("Incorrect model");
                }

                newModel.TransName = model.TransName;

                _repository.Update(newModel);
                await _unitOfWork.SaveAsync();
                return newModel;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(UpdateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdateAsync));
                throw;
            }
        }

        public async override Task DeleteAsync(int id)
        {
            try
            {
                var model = await GetAsync(id);
                if (model.IsFixed)
                {
                    throw new ConstraintException("Current state can not be deleted");   
                }

                _repository.Remove(model);
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
