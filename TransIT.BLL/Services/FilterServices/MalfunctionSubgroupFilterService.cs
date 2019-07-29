using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class MalfunctionSubgroupFilterService : BaseFilterService<MalfunctionSubgroup, MalfunctionGroupDTO>
    {
        public MalfunctionSubgroupFilterService(IQueryRepository<MalfunctionSubgroup> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
