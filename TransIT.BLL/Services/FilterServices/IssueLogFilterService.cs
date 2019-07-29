using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class IssueLogFilterService : BaseFilterService<IssueLog, IssueLogDTO>
    {
        public IssueLogFilterService(IQueryRepository<IssueLog> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
