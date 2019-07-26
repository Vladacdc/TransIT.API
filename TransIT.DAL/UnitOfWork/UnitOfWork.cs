using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.InterfacesRepositories;

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
        public  IRoleRepository RoleRepository { get; }
        public  IUserRepository UserRepository { get; }
        public  IStateRepository StateRepository { get; }
        public  ISupplierRepository SupplierRepository { get; }
        public  IVehicleRepository VehicleRepository { get; }
        public  IVehicleTypeRepository VehicleTypeRepository { get; }
        public  ITokenRepository TokenRepository { get; }
        public  IEmployeeRepository EmployeeRepository { get; }
        public  IPostRepository PostRepository { get; }
        public  ITransitionRepository TransitionRepository { get; }
        public  ILocationRepository LocationRepository { get; }

        public UnitOfWork(DbContext context,
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
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IStateRepository stateRepository,
            ISupplierRepository supplierRepository,
            IVehicleRepository vehicleRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            ITokenRepository tokenRepository,
            IEmployeeRepository employeeRepository,
            IPostRepository postRepository,
            ITransitionRepository transitionRepository,
            ILocationRepository locationRepository)
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
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            StateRepository = stateRepository;
            SupplierRepository = supplierRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
            TokenRepository = tokenRepository;
            EmployeeRepository = employeeRepository;
            PostRepository = postRepository;
            TransitionRepository = transitionRepository;
            LocationRepository = locationRepository;
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
