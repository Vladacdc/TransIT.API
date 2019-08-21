using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    public interface IUnitOfWork 
    {
        IActionTypeRepository ActionTypeRepository { get; }

        ICurrencyRepository CurrencyRepository { get; }

        ICountryRepository CountryRepository { get; }

        IBillRepository BillRepository { get; }

        IDocumentRepository DocumentRepository { get; }

        IIssueRepository IssueRepository { get; }

        IIssueLogRepository IssueLogRepository { get; }

        IMalfunctionRepository MalfunctionRepository { get; }

        IMalfunctionGroupRepository MalfunctionGroupRepository { get; }

        IMalfunctionSubgroupRepository MalfunctionSubgroupRepository { get; }

        IStateRepository StateRepository { get; }

        ISupplierRepository SupplierRepository { get; }

        IVehicleRepository VehicleRepository { get; }

        IVehicleTypeRepository VehicleTypeRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }

        IPostRepository PostRepository { get; }

        ITransitionRepository TransitionRepository { get; }

        ILocationRepository LocationRepository { get; }

        IUserRepository UserRepository { get; }

        RoleManager<Role> RoleManager { get; }

        UserManager<User> UserManager { get; }

        IUnitRepository UnitRepository { get; }

        IWorkTypeRepository WorkTypeRepository { get; }

        IManufacturerRepository ManufacturerRepository { get; }

        Task<int> SaveAsync();
    }
}
