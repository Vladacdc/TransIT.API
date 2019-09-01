using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IQueryRepository<TEntity>
        where TEntity : class, IAuditableEntity, IBaseEntity
    {
        private readonly TransITDBContext _context;
        protected DbSet<TEntity> _entities;

        public BaseRepository(TransITDBContext context)
        {
            _entities = context.Set<TEntity>();
            _context = context;
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return ComplexEntities.SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return ComplexEntities.ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return ComplexEntities.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var entityEntry = await _entities.AddAsync(entity);
            return entityEntry.Entity;
        }

        public async virtual Task<TEntity> RemoveAsync(params object[] keys)
        {
            var model = await _entities.FindAsync(keys);
            if (model != null)
            {
                model = _entities.Remove(model).Entity;
            }

            return model;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            return _entities.Remove(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return _entities.Update(entity).Entity;
        }

        public virtual Task<List<TEntity>> GetRangeAsync(uint index, uint amount)
        {
            return ComplexEntities.Skip((int)index).Take((int)amount).ToListAsync();
        }

        public virtual TEntity UpdateWithIgnoreProperty<TProperty>(
            TEntity entity, Expression<Func<TEntity, TProperty>> ignorePropertyExpression)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(ignorePropertyExpression).IsModified = false;
            return entity;
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                return _entities;
            }
        }

        protected virtual IQueryable<TEntity> ComplexEntities
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// By default no items will be returned, so return false.
        /// </summary>
        /// <param name="keyword">A part of search string.</param>
        /// <returns>An expression predicate for given entity.</returns>
        public virtual Expression<Func<TEntity, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => false;
        }

        public virtual Task<IQueryable<TEntity>> SearchAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<TEntity>();

            foreach (string keyword in strs)
            {
                predicate = predicate.And(MakeFilteringExpression(keyword));
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return ComplexEntities;
        }
    }
}
