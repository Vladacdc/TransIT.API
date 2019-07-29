using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class StateFilterService : BaseFilterService<State, StateDTO>
    {
        public StateFilterService(IQueryRepository<State> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
