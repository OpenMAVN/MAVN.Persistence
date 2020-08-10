using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MAVN.Persistence
{
    public static class DatabaseMigrationExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var contextProvider = scope.ServiceProvider.GetRequiredService<IDbContextProvider>();
                var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();
                try
                {
                    using (var dbContext = contextProvider.CreateDbContext(loggerFactory))
                    {
                        dbContext.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetService<Logger<IHost>>();
                    if (logger != null)
                        logger.LogCritical(ex, "DB migration failure");
                    throw;
                }
            }

            return host;
        }

        public static IServiceProvider MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var contextProvider = serviceProvider.GetRequiredService<IDbContextProvider>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            try
            {
                using (var dbContext = contextProvider.CreateDbContext(loggerFactory))
                {
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetService<Logger<IHost>>();
                if (logger != null)
                    logger.LogCritical(ex, "DB migration failure");
                throw;
            }

            return serviceProvider;
        }
    }
}
