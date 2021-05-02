using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RF.Infrastructure.Data.Utils
{
    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            //Removes the convention where the table names are in plural
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }
        }

        public static void RemoveCascadeDeleteConvention(this ModelBuilder modelBuilder)
        {
            //Removes the convention where the cascade delete is allowed in the foreign key relationships
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public static void AddCascadeDeleteConvention(this ModelBuilder modelBuilder)
        {
            //Removes the convention where the cascade delete is allowed in the foreign key relationships
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}