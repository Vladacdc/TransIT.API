using TransIT.BLL.DTOs;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.BLL.Factories
{
    public class FilterServiceFactory : IFilterServiceFactory
    {
        public IFilterService<ActionTypeDTO> ActionTypeFilterService { get; }

        public IFilterService<BillDTO> BillFilterService { get; }

        public IFilterService<CountryDTO> CountryFilterService { get; }

        public IFilterService<CurrencyDTO> CurrencyFilterService { get; }

        public IFilterService<DocumentDTO> DocumentFilterService { get; }

        public IFilterService<EmployeeDTO> EmployeeFilterService { get; }

        public IFilterService<IssueLogDTO> IssueLogFilterService { get; }

        public IFilterService<IssueDTO> IssueFilterService { get; }

        public IFilterService<LocationDTO> LocationFilterService { get; }

        public IFilterService<MalfunctionDTO> MalfunctionFilterService { get; }

        public IFilterService<MalfunctionGroupDTO> MalfunctionGroupFilterService { get; }

        public IFilterService<MalfunctionSubgroupDTO> MalfunctionSubgroupFilterService { get; }

        public IFilterService<PostDTO> PostFilterService { get; }

        public IFilterService<StateDTO> StateFilterService { get; }

        public IFilterService<SupplierDTO> SupplierFilterService { get; }

        public IFilterService<TransitionDTO> TransitionFilterService { get; }

        public IFilterService<UserDTO> UserFilterService { get; }

        public IFilterService<VehicleDTO> VehicleFilterService { get; }

        public IFilterService<VehicleTypeDTO> VehicleTypeFilterService { get; }

        public IFilterService<UnitDTO> UnitFilterService { get; }

        public IFilterService<WorkTypeDTO> WorkTypeFilterService { get; }

        public IFilterService<ManufacturerDTO> ManufacturerFilterService { get; }

        public IFilterService<TEntityDTO> GetService<TEntityDTO>() where TEntityDTO : class, new()
        {
            switch (typeof(TEntityDTO).Name)
            {
                case nameof(ActionTypeDTO):
                {
                    return (IFilterService<TEntityDTO>) ActionTypeFilterService;
                }

                case nameof(BillDTO):
                {
                    return (IFilterService<TEntityDTO>) BillFilterService;
                }

                case nameof(CountryDTO):
                {
                    return (IFilterService<TEntityDTO>) CountryFilterService;
                }

                case nameof(CurrencyDTO):
                {
                    return (IFilterService<TEntityDTO>) CurrencyFilterService;
                }

                case nameof(DocumentDTO):
                {
                    return (IFilterService<TEntityDTO>) DocumentFilterService;
                }

                case nameof(EmployeeDTO):
                {
                    return (IFilterService<TEntityDTO>) EmployeeFilterService;
                }

                case nameof(IssueLogDTO):
                {
                    return (IFilterService<TEntityDTO>) IssueLogFilterService;
                }

                case nameof(IssueDTO):
                {
                    return (IFilterService<TEntityDTO>) IssueFilterService;
                }

                case nameof(LocationDTO):
                {
                    return (IFilterService<TEntityDTO>) LocationFilterService;
                }

                case nameof(MalfunctionDTO):
                {
                    return (IFilterService<TEntityDTO>) MalfunctionFilterService;
                }

                case nameof(MalfunctionGroupDTO):
                {
                    return (IFilterService<TEntityDTO>) MalfunctionGroupFilterService;
                }

                case nameof(MalfunctionSubgroupDTO):
                {
                    return (IFilterService<TEntityDTO>) MalfunctionSubgroupFilterService;
                }

                case nameof(PostDTO):
                {
                    return (IFilterService<TEntityDTO>) PostFilterService;
                }

                case nameof(StateDTO):
                {
                    return (IFilterService<TEntityDTO>) StateFilterService;
                }

                case nameof(SupplierDTO):
                {
                    return (IFilterService<TEntityDTO>) SupplierFilterService;
                }

                case nameof(TransitionDTO):
                {
                    return (IFilterService<TEntityDTO>) TransitionFilterService;
                }

                case nameof(UserDTO):
                {
                    return (IFilterService<TEntityDTO>) UserFilterService;
                }

                case nameof(VehicleDTO):
                {
                    return (IFilterService<TEntityDTO>) VehicleFilterService;
                }

                case nameof(VehicleTypeDTO):
                {
                    return (IFilterService<TEntityDTO>) VehicleTypeFilterService;
                }

                case nameof(UnitDTO):
                {
                    return (IFilterService<TEntityDTO>) UnitFilterService;
                }

                case nameof(WorkTypeDTO):
                {
                    return (IFilterService<TEntityDTO>) WorkTypeFilterService;
                }

                case nameof(ManufacturerDTO):
                {
                    return (IFilterService<TEntityDTO>) MalfunctionFilterService;
                }

                default:
                {
                    return null;
                }
            }
        }

        public FilterServiceFactory(IFilterService<ActionTypeDTO> actionTypeFilterService,
            IFilterService<BillDTO> billFilterService,
            IFilterService<CountryDTO> countryFilterService,
            IFilterService<CurrencyDTO> currencyFilterService,
            IFilterService<DocumentDTO> documentFilterService,
            IFilterService<EmployeeDTO> employeeFilterService,
            IFilterService<IssueLogDTO> issueLogFilterService,
            IFilterService<IssueDTO> issueFilterService,
            IFilterService<LocationDTO> locationFilterService,
            IFilterService<MalfunctionDTO> malfunctionFilterService,
            IFilterService<MalfunctionGroupDTO> malfunctionGroupFilterService,
            IFilterService<MalfunctionSubgroupDTO> malfunctionSubgroupFilterService,
            IFilterService<PostDTO> postFilterService,
            IFilterService<StateDTO> stateFilterService,
            IFilterService<SupplierDTO> supplierFilterService,
            IFilterService<TransitionDTO> transitionFilterService,
            IFilterService<UserDTO> userFilterService,
            IFilterService<VehicleDTO> vehicleFilterService,
            IFilterService<VehicleTypeDTO> vehicleTypeFilterService, 
            IFilterService<UnitDTO> unitFilterService, 
            IFilterService<WorkTypeDTO> workTypeFilterService,
            IFilterService<ManufacturerDTO> manufacturerFilterService)
        {
            ActionTypeFilterService = actionTypeFilterService;
            BillFilterService = billFilterService;
            CountryFilterService = countryFilterService;
            CurrencyFilterService = currencyFilterService;
            DocumentFilterService = documentFilterService;
            EmployeeFilterService = employeeFilterService;
            IssueLogFilterService = issueLogFilterService;
            IssueFilterService = issueFilterService;
            LocationFilterService = locationFilterService;
            MalfunctionFilterService = malfunctionFilterService;
            MalfunctionGroupFilterService = malfunctionGroupFilterService;
            MalfunctionSubgroupFilterService = malfunctionSubgroupFilterService;
            PostFilterService = postFilterService;
            StateFilterService = stateFilterService;
            SupplierFilterService = supplierFilterService;
            TransitionFilterService = transitionFilterService;
            UserFilterService = userFilterService;
            VehicleFilterService = vehicleFilterService;
            VehicleTypeFilterService = vehicleTypeFilterService;
            UnitFilterService = unitFilterService;
            WorkTypeFilterService = workTypeFilterService;
            ManufacturerFilterService = manufacturerFilterService;
        }
    }
}