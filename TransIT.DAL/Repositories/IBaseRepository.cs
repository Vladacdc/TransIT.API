using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TransIT.DAL.Repositories
{
    public interface IBaseRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetRangeAsync(uint index, uint amount);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> RemoveAsync(params object[] keys);
        TEntity Remove(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity UpdateWithIgnoreProperty<TProperty>(TEntity entity,
            Expression<Func<TEntity, TProperty>> ignorePropertyExpression);
    }
}