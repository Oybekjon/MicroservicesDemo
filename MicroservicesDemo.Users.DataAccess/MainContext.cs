using MicroservicesDemo.Users.DataAccess.Maps;
using MicroservicesDemo.Users.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesDemo.Users.DataAccess
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MainContext()
        {
        }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }

            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}