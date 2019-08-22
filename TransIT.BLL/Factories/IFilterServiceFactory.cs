using TransIT.BLL.DTOs;
using TransIT.BLL.Services;

namespace TransIT.BLL.Factories
{
    public interface IFilterServiceFactory
    {
        IFilterService<ActionTypeDTO> ActionTypeFilterService { get; }

        IFilterService<BillDTO> BillFilterService { get; }

        IFilterService<CountryDTO> CountryFilterService { get; }

        IFilterService<CurrencyDTO> CurrencyFilterService { get; }

        IFilterService<DocumentDTO> DocumentFilterService { get; }

        IFilterService<EmployeeDTO> EmployeeFilterService { get; }

        IFilterService<IssueLogDTO> IssueLogFilterService { get; }

        IFilterService<IssueDTO> IssueFilterService { get; }

        IFilterService<LocationDTO> LocationFilterService { get; }

        IFilterService<MalfunctionDTO> MalfunctionFilterService { get; }

        IFilterService<MalfunctionGroupDTO> MalfunctionGroupFilterService { get; }

        IFilterService<MalfunctionSubgroupDTO> MalfunctionSubgroupFilterService { get; }

        IFilterService<PartDTO> PartFilterService { get; }

        IFilterService<PostDTO> PostFilterService { get; }

        IFilterService<StateDTO> StateFilterService { get; }

        IFilterService<SupplierDTO> SupplierFilterService { get; }

        IFilterService<TransitionDTO> TransitionFilterService { get; }

        IFilterService<UserDTO> UserFilterService { get; }

        IFilterService<VehicleDTO> VehicleFilterService { get; }

        IFilterService<VehicleTypeDTO> VehicleTypeFilterService { get; }

        IFilterService<UnitDTO> UnitFilterService { get; }

        IFilterService<ManufacturerDTO> ManufacturerFilterService { get; }

        IFilterService<TEntityDTO> GetService<TEntityDTO>() where TEntityDTO : class, new();
    }
}