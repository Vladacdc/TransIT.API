using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class UnitFilterService : BaseFilterService<Unit, UnitDTO>

    {
        public UnitFilterService(IQueryRepository<Unit> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}