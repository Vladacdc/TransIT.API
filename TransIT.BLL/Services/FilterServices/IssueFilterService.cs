using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class IssueFilterService : BaseFilterService<Issue, IssueDTO>
    {
        public IssueFilterService(IQueryRepository<Issue> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
