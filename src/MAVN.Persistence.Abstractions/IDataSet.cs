using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MAVN.Persistence.Specifications;

namespace MAVN.Persistence
{
    public interface IDataSet<TEntity>
        where TEntity : class
    {
        void Add(
            TEntity entity);

        void AddRange(
            IEnumerable<TEntity> entities);

        bool Contains(
            ISpecification<TEntity>? specification = null);

        bool Contains(
            Expression<Func<TEntity, bool>> predicate);

        Task<bool> ContainsAsync(
            ISpecification<TEntity>? specification = null);

        Task<bool> ContainsAsync(
            Expression<Func<TEntity, bool>> predicate);

        int Count(
            ISpecification<TEntity>? specification = null);

        int Count(
            Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(
            ISpecification<TEntity>? specification = null);

        Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate);

        long Sum(
            ISpecification<TEntity>? specification,
            Expression<Func<TEntity, long>> selector);

        Task<long> SumAsync(
            ISpecification<TEntity>? specification,
            Expression<Func<TEntity, long>> selector);

        IEnumerable<TEntity> Find(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null);

        Task<IEnumerable<TEntity>> FindAsync(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null);

        TEntity? FindFirstOrDefault(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null);

        Task<TEntity?> FindFirstOrDefaultAsync(
            ISpecification<TEntity>? specification = null,
            IFetchSpecification<TEntity>? fetchSpecification = null);

        void Remove(
            TEntity entity);

        void RemoveRange(
            IEnumerable<TEntity> entities);

        void Update(
            TEntity entity);

        TResult Evaluate<TResult>(
            Func<IQueryable<TEntity>, TResult> func);
    }
}