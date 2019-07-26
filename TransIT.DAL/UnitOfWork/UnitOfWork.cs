using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.InterfacesRepositories;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public  IActionTypeRepository ActionTypeRepository { get; }
        public  ICountryRepository CountryRepository { get; }
        public  ICurrencyRepository CurrencyRepository { get; }
        public  IBillRepository BillRepository { get; }
        public  IDocumentRepository DocumentRepository { get; }
        public  IIssueRepository IssueRepository { get; }
        public  IIssueLogRepository IssueLogRepository { get; }
        public  IMalfunctionRepository MalfunctionRepository { get; }
        public  IMalfunctionGroupRepository MalfunctionGroupRepository { get; }
        public  IMalfunctionSubgroupRepository MalfunctionSubgroupRepository { get; }
        public  IStateRepository StateRepository { get; }
        public  ISupplierRepository SupplierRepository { get; }
        public  IVehicleRepository VehicleRepository { get; }
        public  IVehicleTypeRepository VehicleTypeRepository { get; }
        public  IEmployeeRepository EmployeeRepository { get; }
        public  IPostRepository PostRepository { get; }
        public  ITransitionRepository TransitionRepository { get; set; }
        public  ILocationRepository LocationRepository { get; set; }
        public RoleManager<Role> RoleManager { get; set; }
        public UserManager<User> UserManager { get; set; }

        public UnitOfWork(DbContext context, IActionTypeRepository actionTypeRepository, ICountryRepository countryRepository, ICurrencyRepository currencyRepository, IBillRepository billRepository, IDocumentRepository documentRepository, IIssueRepository issueRepository, IIssueLogRepository issueLogRepository, IMalfunctionRepository malfunctionRepository, IMalfunctionGroupRepository malfunctionGroupRepository, IMalfunctionSubgroupRepository malfunctionSubgroupRepository, IStateRepository stateRepository, ISupplierRepository supplierRepository, IVehicleRepository vehicleRepository, IVehicleTypeRepository vehicleTypeRepository, IEmployeeRepository employeeRepository, IPostRepository postRepository, ITransitionRepository transitionRepository, ILocationRepository locationRepository,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            _context = context;
            ActionTypeRepository = actionTypeRepository;
            CountryRepository = countryRepository;
            CurrencyRepository = currencyRepository;
            BillRepository = billRepository;
            DocumentRepository = documentRepository;
            IssueRepository = issueRepository;
            IssueLogRepository = issueLogRepository;
            MalfunctionRepository = malfunctionRepository;
            MalfunctionGroupRepository = malfunctionGroupRepository;
            MalfunctionSubgroupRepository = malfunctionSubgroupRepository;
            StateRepository = stateRepository;
            SupplierRepository = supplierRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
            EmployeeRepository = employeeRepository;
            PostRepository = postRepository;
            TransitionRepository = transitionRepository;
            LocationRepository = locationRepository;
            RoleManager = roleManager;
            UserManager = userManager;
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
