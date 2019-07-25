using System;
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
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IStateRepository StateRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IVehicleTypeRepository VehicleTypeRepository { get; }
        ITokenRepository TokenRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IPostRepository PostRepository { get; }
        ITransitionRepository TransitionRepository { get; set; }
        ILocationRepository LocationRepository { get; set; }
        RoleManager<Role> RoleManager { get; set; }
        UserManager<User> UserManager { get; set; }
        Task<int> SaveAsync();
    }
}
