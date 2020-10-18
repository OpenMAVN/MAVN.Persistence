using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MAVN.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    public sealed class DataSet<TEntity> : IDataSet<TEntity>
        where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        internal DataSet(
            DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Add(
            TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(
            IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public Task<bool> ContainsAsync(
            ISpecification<TEntity>? specification = null)
        {
            return _dbSet
                .Evaluate(specification)
                .AnyAsync();
        }

        public Task<bool> ContainsAsync(
            Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .AnyAsync();
        }

        public Task<int> CountAsync(
            ISpecification<TEntity>? specification = null)
        {
            return _dbSet
                .Evaluate(specification)
                .CountAsync();
        }

        public Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .CountAsync();
        }

        public IQueryable<TEntity> Find(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null)
        {
            return _dbSet
                .Evaluate(specification)
                .EvaluateFetch(fetchSpecification);
        }

        public void Remove(
            TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(
            IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(
            TEntity entity)
        {
            _dbSet.Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        
        public TResult Evaluate<TResult>(
            Func<IQueryable<TEntity>, TResult> func)
        {
            return func(_dbSet);
        }
    }
}