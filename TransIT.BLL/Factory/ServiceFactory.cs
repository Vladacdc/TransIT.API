using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.BLL.Factory
{
    public class ServiceFactory : IServiceFactory
    {
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

        public IRoleService RoleService { get; }

        public IStateService StateService { get; }

        public ISupplierService SupplierService { get; }

        public ITransitionService TransitionService { get; }

        public IUserService UserService { get; }

        public IVehicleService VehicleService { get; }

        public IVehicleTypeService VehicleTypeService { get; }

        public ServiceFactory(IActionTypeService actionTypeService,
            IBillService billService,
            ICountryService countryService,
            ICurrencyService currencyService,
            IDocumentService documentService,
            IEmployeeService employeeService,
            IIssueLogService issueLogService,
            ILocationService locationService,
            IMalfunctionGroupService malfunctionGroupService,
            IMalfunctionService malfunctionService,
            IMalfunctionSubgroupService malfunctionSubgroupService,
            IPostService postService,
            IRoleService roleService,
            IStateService stateService,
            ISupplierService supplierService,
            ITransitionService transitionService,
            IUserService userService,
            IVehicleService vehicleService,
            IVehicleTypeService vehicleTypeService)
        {
            ActionTypeService = actionTypeService;
            BillService = billService;
            CountryService = countryService;
            CurrencyService = currencyService;
            DocumentService = documentService;
            EmployeeService = employeeService;
            IssueLogService = issueLogService;
            LocationService = locationService;
            MalfunctionGroupService = malfunctionGroupService;
            MalfunctionService = malfunctionService;
            MalfunctionSubgroupService = malfunctionSubgroupService;
            PostService = postService;
            RoleService = roleService;
            StateService = stateService;
            SupplierService = supplierService;
            TransitionService = transitionService;
            UserService = userService;
            VehicleService = vehicleService;
            VehicleTypeService = vehicleTypeService;
        }
    }
}
