using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Bill CRUD service
    /// </summary>
    /// <see cref="IBillService"/>
    public class BillService : CrudService<Bill>, IBillService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public BillService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Bill>> logger,
            IBillRepository repository) : base(unitOfWork, logger, repository) { }
    }
}
