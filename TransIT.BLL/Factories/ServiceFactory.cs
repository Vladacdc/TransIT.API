using System;
using TransIT.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TransIT.BLL.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        
        private IAuthenticationService _authenticationService;

        private IActionTypeService _actionTypeService;

        private IBillService _billService;

        private ICountryService _countryService;

        private ICurrencyService _currencyService;

        private IDocumentService _documentService;

        private IEmployeeService _employeeService;

        private IIssueLogService _issueLogService;

        private IIssueService _issueService;

        private ILocationService _locationService;

        private IMalfunctionGroupService _malfunctionGroupService;

        private IPartService _partService;

        private IPostService _postService;

        private IStateService _stateService;

        private IStatisticsService _statisticsService;

        private ISupplierService _supplierService;

        private ITransitionService _transitionService;

        private IUserService _userService;

        private IVehicleService _vehicleService;

        private IVehicleTypeService _vehicleTypeService;

        private IPartsInService _partsInService;

        private IUnitService _unitService;

        private IWorkTypeService _workTypeService;

        private IManufacturerService _manufacturerService;

        private IMalfunctionSubgroupService _malfunctionSubgroupService;

        private IMalfunctionService _malfunctionService;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAuthenticationService AuthenticationService => _authenticationService ?? (_authenticationService = _serviceProvider.GetService<IAuthenticationService>());

        public IActionTypeService ActionTypeService => _actionTypeService ?? (_actionTypeService = _serviceProvider.GetService<IActionTypeService>());

        public IBillService BillService => _billService ?? (_billService = _serviceProvider.GetService<IBillService>());

        public ICountryService CountryService => _countryService ?? (_countryService = _serviceProvider.GetService<ICountryService>());

        public ICurrencyService CurrencyService => _currencyService ?? (_currencyService = _serviceProvider.GetService<ICurrencyService>());

        public IDocumentService DocumentService => _documentService ?? (_documentService = _serviceProvider.GetService<IDocumentService>());

        public IEmployeeService EmployeeService => _employeeService ?? (_employeeService = _serviceProvider.GetService<IEmployeeService>());

        public IIssueLogService IssueLogService => _issueLogService ?? (_issueLogService = _serviceProvider.GetService<IIssueLogService>());

        public IIssueService IssueService => _issueService ?? (_issueService = _serviceProvider.GetService<IIssueService>());

        public ILocationService LocationService => _locationService ?? (_locationService = _serviceProvider.GetService<ILocationService>());

        public IMalfunctionGroupService MalfunctionGroupService => _malfunctionGroupService ?? (_malfunctionGroupService = _serviceProvider.GetService<IMalfunctionGroupService>());

        public IMalfunctionService MalfunctionService => _malfunctionService ?? (_malfunctionService = _serviceProvider.GetService<IMalfunctionService>());

        public IMalfunctionSubgroupService MalfunctionSubgroupService => _malfunctionSubgroupService ?? (_malfunctionSubgroupService = _serviceProvider.GetService<IMalfunctionSubgroupService>());

        public IPartService PartService => _partService ?? (_partService = _serviceProvider.GetService<IPartService>());

        public IPostService PostService => _postService ?? (_postService = _serviceProvider.GetService<IPostService>());

        public IStateService StateService => _stateService ?? (_stateService = _serviceProvider.GetService<IStateService>());

        public IStatisticsService StatisticService => _statisticsService ?? (_statisticsService = _serviceProvider.GetService<IStatisticsService>());

        public ISupplierService SupplierService => _supplierService ?? (_supplierService = _serviceProvider.GetService<ISupplierService>());

        public ITransitionService TransitionService => _transitionService ?? (_transitionService = _serviceProvider.GetService<ITransitionService>());

        public IUserService UserService => _userService ?? (_userService = _serviceProvider.GetService<IUserService>());

        public IVehicleService VehicleService => _vehicleService ?? (_vehicleService = _serviceProvider.GetService<IVehicleService>());

        public IVehicleTypeService VehicleTypeService => _vehicleTypeService ?? (_vehicleTypeService = _serviceProvider.GetService<IVehicleTypeService>());

        public IPartsInService PartsInService => _partsInService ?? (_partsInService = _serviceProvider.GetService<IPartsInService>());

        public IUnitService UnitService => _unitService ?? (_unitService = _serviceProvider.GetService<IUnitService>());

        public IWorkTypeService WorkTypeService => _workTypeService ?? (_workTypeService = _serviceProvider.GetService<IWorkTypeService>());

        public IManufacturerService ManufacturerService => _manufacturerService ?? (_manufacturerService = _serviceProvider.GetService<IManufacturerService>());
    }
}