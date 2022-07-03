using MicroservicesDemo.Items.DataAccess.Maps;
using MicroservicesDemo.Items.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesDemo.Items.DataAccess
{
    public class ItemsContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }

        public ItemsContext()
        {
        }

        public ItemsContext(DbContextOptions<ItemsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }

            modelBuilder.ApplyConfiguration(new ItemMap());
        }
    }
}