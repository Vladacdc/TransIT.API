using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class ActionTypeFilterService : BaseFilterService<ActionType, ActionTypeDTO>
    {
        public ActionTypeFilterService(IQueryRepository<ActionType> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}