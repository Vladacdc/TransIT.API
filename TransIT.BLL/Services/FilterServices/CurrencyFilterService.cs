using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class CurrencyFilterService : BaseFilterService<Currency, CurrencyDTO>
    {
        public CurrencyFilterService(IQueryRepository<Currency> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
