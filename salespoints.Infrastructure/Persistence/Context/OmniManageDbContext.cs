using Microsoft.EntityFrameworkCore;
using salespoints.Infrastructure.Persistence.Configuration.Table;

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
            modelBuilder.ApplyConfiguration(new UsersConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}