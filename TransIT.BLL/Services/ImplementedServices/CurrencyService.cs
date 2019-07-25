using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Currency CRUD service
    /// </summary>
    /// <see cref="ICurrencyService"/>
    public class CurrencyService : CrudService<Currency>, ICurrencyService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public CurrencyService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Currency>> logger,
            ICurrencyRepository repository) : base(unitOfWork, logger, repository) { }
    }
}
