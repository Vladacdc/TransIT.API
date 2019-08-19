using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransITDBContext _context;

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

        public  ITransitionRepository TransitionRepository { get; }

        public  ILocationRepository LocationRepository { get; }

        public RoleManager<Role> RoleManager { get; }

        public UserManager<User> UserManager { get; }

        public IUserRepository UserRepository { get; }

        public IUnitRepository UnitRepository { get; }

        public UnitOfWork(
            TransITDBContext context,
            IActionTypeRepository actionTypeRepository,
            ICountryRepository countryRepository,
            ICurrencyRepository currencyRepository, 
            IBillRepository billRepository, 
            IDocumentRepository documentRepository,
            IIssueRepository issueRepository,
            IIssueLogRepository issueLogRepository,
            IMalfunctionRepository malfunctionRepository,
            IMalfunctionGroupRepository malfunctionGroupRepository,
            IMalfunctionSubgroupRepository malfunctionSubgroupRepository,
            IStateRepository stateRepository,
            ISupplierRepository supplierRepository,
            IVehicleRepository vehicleRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            IEmployeeRepository employeeRepository,
            IPostRepository postRepository,
            ITransitionRepository transitionRepository,
            ILocationRepository locationRepository,
            IUserRepository userRepository,
            RoleManager<Role> roleManager,
            UserManager<User> userManager, 
            IUnitRepository unitRepository)
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
            UserRepository = userRepository;
            RoleManager = roleManager;
            UserManager = userManager;
            UnitRepository = unitRepository;
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
