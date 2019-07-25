using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Country CRUD service
    /// </summary>
    /// <see cref="ICountryService"/>
    public class CountryService : CrudService<Country>, ICountryService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public CountryService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Country>> logger,
            ICountryRepository repository) : base(unitOfWork, logger, repository) { }
    }
}
