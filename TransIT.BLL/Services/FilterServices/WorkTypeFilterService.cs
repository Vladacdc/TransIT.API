using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class WorkTypeFilterService : BaseFilterService<WorkType, WorkTypeDTO>
    {
        public WorkTypeFilterService(IQueryRepository<WorkType> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
