using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class PartsInFilterService : BaseFilterService<PartIn, PartInDTO>
    {
        public PartsInFilterService(IQueryRepository<PartIn> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}