using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class ManufacturerFilterService : BaseFilterService<Manufacturer ,ManufacturerDTO>
    {
        public ManufacturerFilterService(IQueryRepository<Manufacturer> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}