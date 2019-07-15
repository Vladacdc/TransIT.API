using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services
{    
    public interface IFilterService<TEntity> where TEntity : class, IEntity, new()
    {
        ulong TotalRecordsAmount();
        ulong TotalRecordsAmount(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetQueriedAsync();
        Task<IEnumerable<TEntity>> GetQueriedAsync(DataTableRequestDTO dataFilter);
        Task<IEnumerable<TEntity>> GetQueriedWithWhereAsync(
            DataTableRequestDTO dataFilter,
            Expression<Func<TEntity, bool>> matchExpression);
    }
}
