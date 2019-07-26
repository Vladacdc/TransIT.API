using TransIT.BLL.Services.Interfaces;

namespace TransIT.BLL.Factory
{
    public interface IServiceFactory
    {
        IActionTypeService ActionTypeService { get; }

        IBillService BillService { get; }

        ICountryService CountryService { get; }

        ICurrencyService CurrencyService { get; }

        IDocumentService DocumentService { get; }

        IEmployeeService EmployeeService { get; }

        IIssueLogService IssueLogService { get; }

        IIssueService IssueService { get; }

        ILocationService LocationService { get; }

        IMalfunctionGroupService MalfunctionGroupService { get; }

        IMalfunctionService MalfunctionService { get; }

        IMalfunctionSubgroupService MalfunctionSubgroupService { get; }

        IPostService PostService { get; }

        IStateService StateService { get; }

        ISupplierService SupplierService { get; }

        ITransitionService TransitionService { get; }

        IUserService UserService { get; }

        IVehicleService VehicleService { get; }

        IVehicleTypeService VehicleTypeService { get; }
    }
}
