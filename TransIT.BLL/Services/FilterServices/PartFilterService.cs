using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class PartFilterService : BaseFilterService<Part, PartDTO>
    {
        public PartFilterService(IQueryRepository<Part> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
