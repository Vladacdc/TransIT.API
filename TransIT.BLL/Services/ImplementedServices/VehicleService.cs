using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Service for Vehicle
    /// </summary>
    public class VehicleService : CrudService<Vehicle>, IVehicleService
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public VehicleService(IUnitOfWork unitOfWork,
            ILogger<CrudService<Vehicle>> logger,
            IVehicleRepository repository) : base(unitOfWork, logger, repository) { }
    }
}
