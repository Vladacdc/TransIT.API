using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace TransIT.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransITDBContext _context;

        private readonly IServiceProvider _serviceProvider;

        private IActionTypeRepository _actionTypeRepository;

        private ICountryRepository _countryRepository;

        private ICurrencyRepository _currencyRepository;

        private IBillRepository _billRepository;

        private IDocumentRepository _documentRepository;

        private IIssueRepository _issueRepository;

        private IIssueLogRepository _issueLogRepository;

        private IMalfunctionRepository _malfunctionRepository;

        private IMalfunctionGroupRepository _malfunctionGroupRepository;

        private IMalfunctionSubgroupRepository _malfunctionSubgroupRepository;

        private IStateRepository _stateRepository;

        private ISupplierRepository _supplierRepository;

        private IVehicleRepository _vehicleRepository;

        private IVehicleTypeRepository _vehicleTypeRepository;

        private IEmployeeRepository _employeeRepository;

        private IPartRepository _partRepository;

        private IPostRepository _postRepository;

        private ITransitionRepository _transitionRepository;

        private ILocationRepository _locationRepository;

        private RoleManager<Role> _roleManager;

        private UserManager<User> _userManager;

        private IUserRepository _userRepository;

        private IPartsInRepository _partsInRepository;

        private IUnitRepository _unitRepository;

        private IWorkTypeRepository _workTypeRepository;

        private IManufacturerRepository _manufacturerRepository;

        public UnitOfWork(TransITDBContext context,
                          IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public  IActionTypeRepository ActionTypeRepository => _actionTypeRepository ?? (_actionTypeRepository = _serviceProvider.GetService<IActionTypeRepository>());

        public  ICountryRepository CountryRepository => _countryRepository ?? (_countryRepository = _serviceProvider.GetService<ICountryRepository>());

        public  ICurrencyRepository CurrencyRepository => _currencyRepository ?? (_currencyRepository = _serviceProvider.GetService<ICurrencyRepository>());

        public  IBillRepository BillRepository => _billRepository ?? (_billRepository = _serviceProvider.GetService<IBillRepository>());

        public  IDocumentRepository DocumentRepository => _documentRepository ?? (_documentRepository = _serviceProvider.GetService<IDocumentRepository>());

        public  IIssueRepository IssueRepository => _issueRepository ?? (_issueRepository = _serviceProvider.GetService<IIssueRepository>());

        public  IIssueLogRepository IssueLogRepository => _issueLogRepository ?? (_issueLogRepository = _serviceProvider.GetService<IIssueLogRepository>());

        public  IMalfunctionRepository MalfunctionRepository => _malfunctionRepository ?? (_malfunctionRepository = _serviceProvider.GetService<IMalfunctionRepository>());

        public  IMalfunctionGroupRepository MalfunctionGroupRepository => _malfunctionGroupRepository ?? (_malfunctionGroupRepository = _serviceProvider.GetService<IMalfunctionGroupRepository>());

        public  IMalfunctionSubgroupRepository MalfunctionSubgroupRepository => _malfunctionSubgroupRepository ?? (_malfunctionSubgroupRepository = _serviceProvider.GetService<IMalfunctionSubgroupRepository>());

        public  IStateRepository StateRepository => _stateRepository ?? (_stateRepository = _serviceProvider.GetService<IStateRepository>());

        public  ISupplierRepository SupplierRepository => _supplierRepository ?? (_supplierRepository = _serviceProvider.GetService<ISupplierRepository>());

        public  IVehicleRepository VehicleRepository => _vehicleRepository ?? (_vehicleRepository = _serviceProvider.GetService<IVehicleRepository>());

        public  IVehicleTypeRepository VehicleTypeRepository => _vehicleTypeRepository ?? (_vehicleTypeRepository = _serviceProvider.GetService<IVehicleTypeRepository>());

        public  IEmployeeRepository EmployeeRepository => _employeeRepository ?? (_employeeRepository = _serviceProvider.GetService<IEmployeeRepository>());

        public IPartRepository PartRepository => _partRepository ?? (_partRepository = _serviceProvider.GetService<IPartRepository>());

        public  IPostRepository PostRepository => _postRepository ?? (_postRepository = _serviceProvider.GetService<IPostRepository>());

        public  ITransitionRepository TransitionRepository => _transitionRepository ?? (_transitionRepository = _serviceProvider.GetService<ITransitionRepository>());

        public  ILocationRepository LocationRepository => _locationRepository ?? (_locationRepository = _serviceProvider.GetService<ILocationRepository>());

        public RoleManager<Role> RoleManager => _roleManager ?? (_roleManager = _serviceProvider.GetService<RoleManager<Role>>());

        public UserManager<User> UserManager => _userManager ?? (_userManager = _serviceProvider.GetService<UserManager<User>>());

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = _serviceProvider.GetService<IUserRepository>());

        public IPartsInRepository PartsInRepository => _partsInRepository ?? (_partsInRepository = _serviceProvider.GetService<IPartsInRepository>());

        public IUnitRepository UnitRepository => _unitRepository ?? (_unitRepository = _serviceProvider.GetService<IUnitRepository>());

        public IWorkTypeRepository WorkTypeRepository => _workTypeRepository ?? (_workTypeRepository = _serviceProvider.GetService<IWorkTypeRepository>());

        public IManufacturerRepository ManufacturerRepository => _manufacturerRepository ?? (_manufacturerRepository = _serviceProvider.GetService<IManufacturerRepository>());

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
