using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class MalfunctionGroupFilterService : BaseFilterService<MalfunctionGroup, MalfunctionGroupDTO>
    {
        public MalfunctionGroupFilterService(IQueryRepository<MalfunctionGroup> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
