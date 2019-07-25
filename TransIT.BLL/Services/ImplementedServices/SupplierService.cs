using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Supplier CRUD service
    /// </summary>
    /// <see cref="ISupplierService"/>
    public class SupplierService : CrudService<Supplier>, ISupplierService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public SupplierService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Supplier>> logger,
            ISupplierRepository repository) : base(unitOfWork, logger, repository) { }
    }
}
