using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class TransitionFilterService : BaseFilterService<Transition, TransitionDTO>
    {
        public TransitionFilterService(IQueryRepository<Transition> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
