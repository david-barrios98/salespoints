using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salespoints.Core.Domain.Entities;

namespace salespoints.Infrastructure.Persistence.Configurations
{
    public class SalesPointConfiguration : IEntityTypeConfiguration<SalesPoint>
    {
        public void Configure(EntityTypeBuilder<SalesPoint> builder)
        {
            builder.ToTable("salespoints", "sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.Code)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("code");

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(250)
                   .HasColumnName("name");

            builder.Property(s => s.Address)
                   .HasMaxLength(500)
                   .HasColumnName("address");

            builder.Property(s => s.Active)
                   .HasColumnName("active")
                   .HasDefaultValueSql("((1))");

            builder.Property(s => s.Create)
                   .HasColumnName("create")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.Update)
                   .HasColumnName("update")
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}