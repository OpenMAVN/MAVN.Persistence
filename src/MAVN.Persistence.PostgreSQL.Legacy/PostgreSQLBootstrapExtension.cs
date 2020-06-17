using System;
using System.Data.Common;
using Autofac;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence.PostgreSQL.Legacy
{
    [PublicAPI]
    public static class PostgreSQLBootstrapExtension
    {
        public static void RegisterPostgreSQL<T>(
            this ContainerBuilder builder,
            string dbConnString,
            Func<string, T> connStringCreator,
            Func<DbConnection, T> dbConnectionCreator)
            where T : PostgreSQLContext
        {
            using (var context = connStringCreator(dbConnString))
            {
                context.IsTraceEnabled = true;

                context.Database.Migrate();
            }

            builder.RegisterInstance(
                    new PostgreSQLContextFactory<T>(
                        dbConnString,
                        connStringCreator,
                        dbConnectionCreator))
                .AsSelf()
                .As<IDbContextFactory<T>>()
                .As<ITransactionRunner>()
                .SingleInstance();
        }
    }
}