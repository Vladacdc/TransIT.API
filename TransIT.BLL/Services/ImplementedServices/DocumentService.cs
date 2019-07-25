using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Helpers.FileStorageLogger;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Document CRUD service
    /// </summary>
    /// <see cref="IDocumentService"/>
    public class DocumentService : CrudService<Document>, IDocumentService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        private readonly IFileStorageLogger _storageLogger;

        public DocumentService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Document>> logger,
            IDocumentRepository repository) : base(unitOfWork, logger, repository) {
            _storageLogger = LoggerProviderFactory.GetFileStorageLogger();
        }

        public Task<IEnumerable<Document>> GetRangeByIssueLogIdAsync(int issueLogId) =>
            _repository.GetAllAsync(i => i.IssueLogId == issueLogId);

        public override async Task DeleteAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                await Task.Run(() => _storageLogger.Delete(result.Path));
                _repository.Remove(result);
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


