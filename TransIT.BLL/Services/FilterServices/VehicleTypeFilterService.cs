using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class VehicleTypeFilterService : BaseFilterService<VehicleType, VehicleTypeDTO>
    {
        public VehicleTypeFilterService(IQueryRepository<VehicleType> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}