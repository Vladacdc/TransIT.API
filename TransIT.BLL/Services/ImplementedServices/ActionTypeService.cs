using System;
using System.Data;
using System.Data.SqlClient;
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
    /// Malfunction Group CRUD service
    /// </summary>
    /// <see cref="IActionTypeService"/>
    public class ActionTypeService : CrudService<ActionType>, IActionTypeService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public ActionTypeService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<ActionType>> logger,
            IActionTypeRepository repository) : base(unitOfWork, logger, repository) { }
        
        public async override Task<ActionType> UpdateAsync(ActionType model)
        {
            try
            {
                var newModel = await GetAsync(model.Id);
                if (newModel.IsFixed)
                {
                    throw new ConstraintException("Current state can not be edited");
                }
                if (model.IsFixed)
                {
                    throw new ArgumentException("Incorrect model");
                }
                newModel.Name = model.Name;

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
