using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class LocationFilterService : BaseFilterService<Location, LocationDTO>
    {
        public LocationFilterService(IQueryRepository<Location> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
