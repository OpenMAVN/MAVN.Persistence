using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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

        public bool Contains(ISpecification<TEntity>? specification = null)
        {
            return _dbSet
                .Evaluate(specification)
                .Any();
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Any();
        }

        public Task<bool> ContainsAsync(
            ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        {
            return _dbSet
                .Evaluate(specification)
                .AnyAsync(cancellationToken);
        }

        public Task<bool> ContainsAsync(
            Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbSet
                .Where(predicate)
                .AnyAsync(cancellationToken);
        }

        public int Count(ISpecification<TEntity>? specification = null)
        {
            return _dbSet
                .Evaluate(specification)
                .Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Count();
        }

        public Task<int> CountAsync(
            ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        {
            return _dbSet
                .Evaluate(specification)
                .CountAsync(cancellationToken);
        }

        public Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbSet
                .Where(predicate)
                .CountAsync(cancellationToken);
        }

        public long Sum(
            ISpecification<TEntity>? specification,
            Expression<Func<TEntity, long>> selector)
        {
            return _dbSet
                .Evaluate(specification)
                .Select(selector)
                .Sum();
        }

        public Task<long> SumAsync(
            ISpecification<TEntity>? specification,
            Expression<Func<TEntity, long>> selector,
            CancellationToken cancellationToken = default)
        {
            return _dbSet
                .Evaluate(specification)
                .Select(selector)
                .SumAsync(cancellationToken);
        }

        public IEnumerable<TEntity> Find(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null)
        {
            return _dbSet
                .Evaluate(specification)
                .EvaluateFetch(fetchSpecification);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Evaluate(specification)
                .EvaluateFetch(fetchSpecification)
                .ToListAsync(cancellationToken);
        }

        public TEntity? FindFirstOrDefault(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null)
        {
            return _dbSet
                .Evaluate(specification)
                .EvaluateFetch(fetchSpecification)
                .FirstOrDefault();
        }

        public async Task<TEntity?> FindFirstOrDefaultAsync(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Evaluate(specification)
                .EvaluateFetch(fetchSpecification)
                .FirstOrDefaultAsync(cancellationToken);
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