﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MAVN.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    internal class IncludeExpression<T> : IIncludeExpression<T>
    {
        private static readonly MethodInfo IncludeMethodInfo
            = typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Include))
                .Single(
                    mi =>
                        mi.GetGenericArguments().Count() == 2
                        && mi.GetParameters().Any(
                            pi => pi.Name == "navigationPropertyPath" && pi.ParameterType != typeof(string)));
        private static readonly MethodInfo ThenIncludeAfterReferenceMethodInfo
            = typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(ThenInclude))
                .Single(
                    mi => mi.GetGenericArguments().Count() == 3
                        && mi.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericParameter);

        private (Type, Expression) _parentInclude;
        private List<(Type, Type, Expression)> _thenIncludes = new List<(Type, Type, Expression)>();

        public IEnumerable<T> AddIncludes(IEnumerable<T> items)
        {
            var query = items as IQueryable<T>;
            if (query == null)
                throw new ArgumentException("Input param must be a proper IQueryable<T>");

            var (type, expr) = _parentInclude;
            query = query.Provider.CreateQuery<T>(
                Expression.Call(
                    instance: null,
                    method: IncludeMethodInfo.MakeGenericMethod(typeof(T), type),
                    arguments: new[] { query.Expression, Expression.Quote(expr) }));

            foreach (var (typeFrom, typeTo, childExpr) in _thenIncludes)
            {
                query = query.Provider.CreateQuery<T>(
                    Expression.Call(
                        instance: null,
                        method: ThenIncludeAfterReferenceMethodInfo.MakeGenericMethod(
                            typeof(T), typeFrom, typeTo),
                        arguments: new[] { query.Expression, Expression.Quote(childExpr) }));
            }

            return query;
        }

        internal void Include<TEntity, TProperty>(
            Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            _parentInclude = (typeof(TProperty), navigationPropertyPath);
        }

        internal void ThenInclude<TPreviousProperty, TProperty>(
            Expression<Func<TPreviousProperty, TProperty>> keySelector)
        {
            _thenIncludes.Add((typeof(TPreviousProperty), typeof(TProperty), keySelector));
        }
    }
}
