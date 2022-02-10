using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MAVN.Persistence.Specifications;

namespace MAVN.Persistence
{
    public sealed class DataSet<TEntity> : IDataSet<TEntity>
        where TEntity : class
    {
        public void Add(
            TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(
            IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ContainsAsync(
            ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ContainsAsync(
            Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(
            ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAsync(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(
            TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(
            IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(
            TEntity entity)
        {
            throw new NotImplementedException();
        }
        
        public TResult Evaluate<TResult>(
            Func<IQueryable<TEntity>, TResult> func)
        {
            throw new NotImplementedException();
        }

        public TEntity? FindFirstOrDefault(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> FindFirstOrDefaultAsync(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool Contains(ISpecification<TEntity>? specification = null)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count(ISpecification<TEntity>? specification = null)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public long Sum(ISpecification<TEntity>? specification, Expression<Func<TEntity, long>> selector)
        {
            throw new NotImplementedException();
        }

        public Task<long> SumAsync(
            ISpecification<TEntity>? specification,
            Expression<Func<TEntity, long>> selector,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}