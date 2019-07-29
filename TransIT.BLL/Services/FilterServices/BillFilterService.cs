using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class BillFilterService : BaseFilterService<Bill, BillDTO>
    {
        public BillFilterService(IQueryRepository<Bill> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
