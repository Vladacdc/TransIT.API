﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
            _context = context;
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return ComplexEntities.SingleOrDefaultAsync(t => t.Id.Equals(id));
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TEntity>>(ComplexEntities);
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<TEntity>>(ComplexEntities.Where(predicate));
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await Entities.AddAsync(entity)).Entity;
        }

        public virtual TEntity Remove(params object[] keys)
        {
            var model = Entities.Find(keys);
            if (model != null)
            {
                model = Entities.Remove(model).Entity;
            }

            return model;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            return Entities.Remove(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return Entities.Update(entity).Entity;
        }

        public virtual Task<IEnumerable<TEntity>> GetRangeAsync(uint index, uint amount)
        {
            return Task.FromResult<IEnumerable<TEntity>>(ComplexEntities.Skip((int)index).Take((int)amount));
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
                return _entities ?? (_entities = _context.Set<TEntity>());
            }
        }

        protected virtual IQueryable<TEntity> ComplexEntities
        {
            get
            {
                return Entities;
            }
        }
        
        public abstract Task<IQueryable<TEntity>> SearchExpressionAsync(IEnumerable<string> strs);

        public IQueryable<TEntity> GetQueryable()
        {
            return ComplexEntities;
        }
    }
}
