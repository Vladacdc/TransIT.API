using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Employee CRUD service
    /// </summary>
    /// <see cref="IEmployeeService"/>
    public class EmployeeService : CrudService<Employee>, IEmployeeService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public EmployeeService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Employee>> logger,
            IEmployeeRepository repository) : base(unitOfWork, logger, repository) { }
    }
}
