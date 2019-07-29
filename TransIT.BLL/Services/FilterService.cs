using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;
using TransIT.BLL.Helpers;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services
{
    public class FilterService<TEntity> : IFilterService<TEntity>
        where TEntity : class, IAuditableEntity, new()
    {        
        protected readonly IQueryRepository<TEntity> _queryRepository;
        
        public FilterService(IQueryRepository<TEntity> queryRepository)
        {
            _queryRepository = queryRepository;
        }


        public ulong TotalRecordsAmount() =>
            (ulong)_queryRepository
                .GetQueryable()
                .LongCount(); 
              
        public virtual async Task<IEnumerable<TEntity>> GetQueriedAsync(DataTableRequestDTO dataFilter) => 
            await GetQueriedAsync(dataFilter, await DetermineDataSource(dataFilter));

        protected virtual Task<IQueryable<TEntity>> GetQueriedAsync(
            DataTableRequestDTO dataFilter,
            IQueryable<TEntity> dataSource) =>
            Task.FromResult(ProcessQuery(dataFilter, dataSource));

        private async Task<IQueryable<TEntity>> DetermineDataSource(DataTableRequestDTO dataFilter) =>
            dataFilter.Search != null
            && !string.IsNullOrEmpty(dataFilter.Search.Value)
                ? await _queryRepository.SearchExpressionAsync(
                      dataFilter.Search.Value
                        .Split(new[] {' ', ',', '.'}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim().ToUpperInvariant())
                      )
                : _queryRepository.GetQueryable();

        private IQueryable<TEntity> ProcessQuery(DataTableRequestDTO dataFilter, IQueryable<TEntity> data)
        {
            if (dataFilter.Filters != null
                && dataFilter.Filters.Any())
                data = ProcessQueryFilter(dataFilter.Filters, data);
            
            if (dataFilter.Columns != null
                && dataFilter.Order != null
                && dataFilter.Columns.Any()
                && dataFilter.Order.Any()
                && dataFilter.Order.All(o =>
                    o != null
                    && !string.IsNullOrEmpty(o.Dir)
                    && o.Column >= 0))
                data = TableOrderBy(dataFilter, data);
            
            if (dataFilter.Start >= 0
                && dataFilter.Length > 0)
                data = data
                    .Skip(dataFilter.Start)
                    .Take(dataFilter.Length);

            return data;
        }

        private IQueryable<TEntity> ProcessQueryFilter(
            IEnumerable<DataTableRequestDTO.FilterType> filters,
            IQueryable<TEntity> data)
        {
            filters.ToList().ForEach(filter =>
                data = TableWhereEqual(filter, data)
                );
            return data;
        }

        private IQueryable<TEntity> TableOrderBy(DataTableRequestDTO dataFilter, IQueryable<TEntity> data)
        {
            data = data.OrderBy(
                dataFilter.Columns[dataFilter.Order[0].Column].Data,
                dataFilter.Order[0].Dir == DataTableRequestDTO.DataTableDescending
                );
            for (var i = 1; i < dataFilter.Order.Length; ++i)
                data = data.ThenBy(
                    dataFilter.Columns[dataFilter.Order[i].Column].Data,
                    dataFilter.Order[i].Dir == DataTableRequestDTO.DataTableDescending
                    );
            return data;
        }

        private IQueryable<TEntity> TableWhereEqual(DataTableRequestDTO.FilterType filter, IQueryable<TEntity> data)
        {
            var value = FilterProcessingHelper.DetectStringType(filter.Value);
            return value == null
                ? data
                : data.Where(filter.EntityPropertyPath, value, filter.Operator);
        }
    }
}
