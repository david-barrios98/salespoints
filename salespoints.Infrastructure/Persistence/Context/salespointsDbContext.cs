using Microsoft.EntityFrameworkCore;
using salespoints.Core.Domain.Entities;
using salespoints.Core.Domain.Entities.Inventory;
using salespoints.Infrastructure.Persistence.Configuration.Table;
using salespoints.Infrastructure.Persistence.Configurations;

namespace salespoints.Infrastructure.Persistence.Adapters
{
    public class salespointsDbContext : DbContext
    {
        public salespointsDbContext(DbContextOptions<salespointsDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }

        public DbSet<InventoryItem> InventoryItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UsersConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SalesPointConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}