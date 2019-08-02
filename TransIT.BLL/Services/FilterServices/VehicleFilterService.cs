using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class VehicleFilterService : BaseFilterService<Vehicle, VehicleDTO>
    {
        public VehicleFilterService(IQueryRepository<Vehicle> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}