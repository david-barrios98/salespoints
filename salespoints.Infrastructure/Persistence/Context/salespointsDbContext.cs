using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UsersConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SalesPointConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}