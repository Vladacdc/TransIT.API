using System;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TransIT.BLL.Factories
{
    public class FilterServiceFactory : IFilterServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private IFilterService<ActionTypeDTO> _actionTypeFilterService;

        private IFilterService<BillDTO> _billFilterService;

        private IFilterService<CountryDTO> _countryFilterService;

        private IFilterService<CurrencyDTO> _currencyFilterService;

        private IFilterService<DocumentDTO> _documentFilterService;

        private IFilterService<EmployeeDTO> _employeeFilterService;

        private IFilterService<IssueLogDTO> _issueLogFilterService;

        private IFilterService<IssueDTO> _issueFilterService;

        private IFilterService<LocationDTO> _locationFilterService;

        private IFilterService<MalfunctionDTO> _malfunctionFilterService;

        private IFilterService<MalfunctionGroupDTO> _malfunctionGroupFilterService;

        private IFilterService<MalfunctionSubgroupDTO> _malfunctionSubgroupFilterService;

        private IFilterService<PartDTO> _partFilterService;

        private IFilterService<PostDTO> _postFilterService;

        private IFilterService<StateDTO> _stateFilterService;

        private IFilterService<SupplierDTO> _supplierFilterService;

        private IFilterService<TransitionDTO> _transitionFilterService;

        private IFilterService<UserDTO> _userFilterService;

        private IFilterService<VehicleDTO> _vehicleFilterService;

        private IFilterService<VehicleTypeDTO> _vehicleTypeFilterService;

        private IFilterService<UnitDTO> _unitFilterService;

        private IFilterService<WorkTypeDTO> _workTypeFilterService;

        private IFilterService<ManufacturerDTO> _manufacturerFilterService;

        private IFilterService<PartInDTO> _partInFilterService;
        public FilterServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFilterService<ActionTypeDTO> ActionTypeFilterService => _actionTypeFilterService ?? (_actionTypeFilterService = _serviceProvider.GetService<IFilterService<ActionTypeDTO>>());

        public IFilterService<BillDTO> BillFilterService => _billFilterService ?? (_billFilterService = _serviceProvider.GetService<IFilterService<BillDTO>>());

        public IFilterService<CountryDTO> CountryFilterService => _countryFilterService ?? (_countryFilterService = _serviceProvider.GetService<IFilterService<CountryDTO>>());

        public IFilterService<CurrencyDTO> CurrencyFilterService => _currencyFilterService ?? (_currencyFilterService = _serviceProvider.GetService<IFilterService<CurrencyDTO>>());

        public IFilterService<DocumentDTO> DocumentFilterService => _documentFilterService ?? (_documentFilterService = _serviceProvider.GetService<IFilterService<DocumentDTO>>());

        public IFilterService<EmployeeDTO> EmployeeFilterService => _employeeFilterService ?? (_employeeFilterService = _serviceProvider.GetService<IFilterService<EmployeeDTO>>());

        public IFilterService<IssueLogDTO> IssueLogFilterService => _issueLogFilterService ?? (_issueLogFilterService = _serviceProvider.GetService<IFilterService<IssueLogDTO>>());

        public IFilterService<IssueDTO> IssueFilterService => _issueFilterService ?? (_issueFilterService = _serviceProvider.GetService<IFilterService<IssueDTO>>());

        public IFilterService<LocationDTO> LocationFilterService => _locationFilterService ?? (_locationFilterService = _serviceProvider.GetService<IFilterService<LocationDTO>>());

        public IFilterService<MalfunctionDTO> MalfunctionFilterService => _malfunctionFilterService ?? (_malfunctionFilterService = _serviceProvider.GetService<IFilterService<MalfunctionDTO>>());

        public IFilterService<MalfunctionGroupDTO> MalfunctionGroupFilterService => _malfunctionGroupFilterService ?? (_malfunctionGroupFilterService = _serviceProvider.GetService<IFilterService<MalfunctionGroupDTO>>());

        public IFilterService<MalfunctionSubgroupDTO> MalfunctionSubgroupFilterService => _malfunctionSubgroupFilterService ?? (_malfunctionSubgroupFilterService = _serviceProvider.GetService<IFilterService<MalfunctionSubgroupDTO>>());

        public IFilterService<PartDTO> PartFilterService => _partFilterService ?? (_partFilterService = _serviceProvider.GetService<IFilterService<PartDTO>>());

        public IFilterService<PostDTO> PostFilterService => _postFilterService ?? (_postFilterService = _serviceProvider.GetService<IFilterService<PostDTO>>());

        public IFilterService<StateDTO> StateFilterService => _stateFilterService ?? (_stateFilterService = _serviceProvider.GetService<IFilterService<StateDTO>>());

        public IFilterService<SupplierDTO> SupplierFilterService => _supplierFilterService ?? (_supplierFilterService = _serviceProvider.GetService<IFilterService<SupplierDTO>>());

        public IFilterService<TransitionDTO> TransitionFilterService => _transitionFilterService ?? (_transitionFilterService = _serviceProvider.GetService<IFilterService<TransitionDTO>>());

        public IFilterService<UserDTO> UserFilterService => _userFilterService ?? (_userFilterService = _serviceProvider.GetService<IFilterService<UserDTO>>());

        public IFilterService<VehicleDTO> VehicleFilterService => _vehicleFilterService ?? (_vehicleFilterService = _serviceProvider.GetService<IFilterService<VehicleDTO>>());

        public IFilterService<VehicleTypeDTO> VehicleTypeFilterService => _vehicleTypeFilterService ?? (_vehicleTypeFilterService = _serviceProvider.GetService<IFilterService<VehicleTypeDTO>>());

        public IFilterService<UnitDTO> UnitFilterService => _unitFilterService ?? (_unitFilterService = _serviceProvider.GetService<IFilterService<UnitDTO>>());

        public IFilterService<WorkTypeDTO> WorkTypeFilterService => _workTypeFilterService ?? (_workTypeFilterService = _serviceProvider.GetService<IFilterService<WorkTypeDTO>>());

        public IFilterService<ManufacturerDTO> ManufacturerFilterService => _manufacturerFilterService ?? (_manufacturerFilterService = _serviceProvider.GetService<IFilterService<ManufacturerDTO>>());

        public IFilterService<PartInDTO> PartInFilterService => _partInFilterService ?? (_partInFilterService = _serviceProvider.GetService<IFilterService<PartInDTO>>());

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
                    return (IFilterService<TEntityDTO>) ManufacturerFilterService;
                }

                case nameof(PartDTO):
                {
                    return (IFilterService<TEntityDTO>) PartFilterService;
                }

                case nameof(PartInDTO):
                {
                    return (IFilterService<TEntityDTO>)PartInFilterService;
                }

                default:
                {
                    return null;
                }
            }
        }
    }
}