using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RF.Domain.Common;
using RF.Domain.Entities;
using RF.Infrastructure.Data.Utils;

namespace RF.Infrastructure.Data.Adapters.EntityFrameworkCore.Contexts
{
    public abstract class EFBaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EFBaseContext"/> class.
        /// Do not use for test only
        /// </summary>
        protected EFBaseContext()
        {
        }

        protected EFBaseContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
            modelBuilder.Entity<Source>()
                .HasMany(x => x.Templates)
                .WithOne(x => x.Source);
            modelBuilder.RemoveCascadeDeleteConvention();
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x =>
                x.Entity is RFBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        ((RFBaseEntity)entity.Entity).CreatedOn = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        ((RFBaseEntity)entity.Entity).ModifiedOn = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}