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

        public IPartService PartService { get; }

        public IPostService PostService { get; }

        public IStateService StateService { get; }

        public IStatisticsService StatisticService { get; }

        public ISupplierService SupplierService { get; }

        public ITransitionService TransitionService { get; }

        public IUserService UserService { get; }

        public IVehicleService VehicleService { get; }

        public IVehicleTypeService VehicleTypeService { get; }

        public IPartsInService PartsInService { get; }
        public IUnitService UnitService { get; }

        public IWorkTypeService WorkTypeService { get; }

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
            IPartService partService,
            IStateService stateService,
            IStatisticsService statisticService,
            ISupplierService supplierService,
            ITransitionService transitionService,
            IUserService userService,
            IVehicleService vehicleService, 
            IPartsInService partsInService,
            IUnitService unitService, 
            IVehicleTypeService vehicleTypeService,
            IWorkTypeService workTypeService,
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
            PartService = partService;
            PostService = postService;
            StateService = stateService;
            StatisticService = statisticService;
            SupplierService = supplierService;
            TransitionService = transitionService;
            UserService = userService;
            VehicleService = vehicleService;
            VehicleTypeService = vehicleTypeService;
            PartsInService = partsInService;
            UnitService = unitService;
            WorkTypeService = workTypeService;
            ManufacturerService = manufacturerService;
        }
    }
}