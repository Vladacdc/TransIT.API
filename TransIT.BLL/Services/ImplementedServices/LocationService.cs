using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    public class LocationService : CrudService<int, Location>, ILocationService
    {
        public LocationService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<int, Location>> logger,
            ILocationRepository repository) : base(unitOfWork, logger, repository) { }
    }
}
