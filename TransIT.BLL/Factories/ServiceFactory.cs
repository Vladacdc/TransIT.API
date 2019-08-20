using TransIT.BLL.Services.Interfaces;

namespace TransIT.BLL.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        public IAuthenticationService AuthenticationService { get; }

        public IActionTypeService ActionTypeService { get; }

        public IBillService BillService { get; }

        public ICountryService CountryService { get; }

        public ICurrencyService CurrencyService { get; }

        public IDocumentService DocumentService { get; }

        public IEmployeeService EmployeeService { get; }

        public IIssueLogService IssueLogService { get; }

        public IIssueService IssueService { get; }

        public ILocationService LocationService { get; }

        public IMalfunctionGroupService MalfunctionGroupService { get; }

        public IMalfunctionService MalfunctionService { get; }

        public IMalfunctionSubgroupService MalfunctionSubgroupService { get; }

        public IPostService PostService { get; }

        public IStateService StateService { get; }

        public IStatisticsService StatisticService { get; }

        public ISupplierService SupplierService { get; }

        public ITransitionService TransitionService { get; }

        public IUserService UserService { get; }

        public IVehicleService VehicleService { get; }

        public IVehicleTypeService VehicleTypeService { get; }

        public IUnitService UnitService { get; }

        public IManufacturerService ManufacturerService { get; }

        public ServiceFactory(IAuthenticationService authenticationService,
            IActionTypeService actionTypeService,
            IBillService billService,
            ICountryService countryService,
            ICurrencyService currencyService,
            IDocumentService documentService,
            IEmployeeService employeeService,
            IIssueLogService issueLogService,
            IIssueService issueService,
            ILocationService locationService,
            IMalfunctionGroupService malfunctionGroupService,
            IMalfunctionService malfunctionService,
            IMalfunctionSubgroupService malfunctionSubgroupService,
            IPostService postService,
            IStateService stateService,
            IStatisticsService statisticService,
            ISupplierService supplierService,
            ITransitionService transitionService,
            IUserService userService,
            IVehicleService vehicleService,
            IVehicleTypeService vehicleTypeService, 
            IUnitService unitService, 
            IManufacturerService manufacturerService)
        {
            AuthenticationService = authenticationService;
            ActionTypeService = actionTypeService;
            BillService = billService;
            CountryService = countryService;
            CurrencyService = currencyService;
            DocumentService = documentService;
            EmployeeService = employeeService;
            IssueLogService = issueLogService;
            IssueService = issueService;
            LocationService = locationService;
            MalfunctionGroupService = malfunctionGroupService;
            MalfunctionService = malfunctionService;
            MalfunctionSubgroupService = malfunctionSubgroupService;
            PostService = postService;
            StateService = stateService;
            StatisticService = statisticService;
            SupplierService = supplierService;
            TransitionService = transitionService;
            UserService = userService;
            VehicleService = vehicleService;
            VehicleTypeService = vehicleTypeService;
            UnitService = unitService;
            ManufacturerService = manufacturerService;
        }
    }
}