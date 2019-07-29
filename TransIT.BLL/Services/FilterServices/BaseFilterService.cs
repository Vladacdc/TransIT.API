using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class BaseFilterService<TEntity, TDto> : IFilterService<TDto>
        where TEntity : class, IAuditableEntity, new()
        where TDto : class, new()
    {
        private readonly IMapper _mapper;
        private readonly FilterService<TEntity> _filterService;

        public BaseFilterService(IQueryRepository<TEntity> repository, IMapper mapper)
        {
            _filterService = new FilterService<TEntity>(repository);
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetQueriedAsync(DataTableRequestDTO dataFilter)
        {
            return _mapper.Map<IEnumerable<TDto>>(
                await _filterService.GetQueriedAsync(dataFilter)
            );
        }

        public Task<ulong> TotalRecordsAmountAsync()
        {
            return _filterService.TotalRecordsAmount();
        }
    }
}
