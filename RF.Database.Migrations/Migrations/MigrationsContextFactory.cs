using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RF.Infrastructure.Configurations.Extensions;
using RF.Infrastructure.Data.Adapters.EntityFrameworkCore;

namespace RF.Database.Migrations.Migrations
{
    internal class MigrationsContextFactory : IDesignTimeDbContextFactory<EFCoreContext>
    {
        public EFCoreContext CreateDbContext(string[] args)
        {
            return Create();
        }

        private EFCoreContext Create()
        {
            var configuration = ConfigurationsExtensionMethods.BuildSharedConfiguration();

            var connectionString = ConfigurationsExtensionMethods.GetConnectionStringFromSharedConfigurations(configuration);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Could not find a connection string");
            }

            var optionsBuilder = new DbContextOptionsBuilder<EFCoreContext>();

            optionsBuilder
                .UseNpgsql(connectionString,
                    x =>
                    {
                        x.MigrationsAssembly("RF.Database.Migrations");
                        x.MigrationsHistoryTable("_RFMigrationHistory", "public");
                    });

            return new EFCoreContext(optionsBuilder.Options);
        }
    }
}