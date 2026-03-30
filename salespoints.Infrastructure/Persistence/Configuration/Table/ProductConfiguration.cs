using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salespoints.Core.Domain.Entities;

namespace salespoints.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products", "catalog");

            builder.HasKey(p => p.id);
            builder.Property(p => p.id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.code)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("code");

            builder.Property(p => p.name)
                   .IsRequired()
                   .HasMaxLength(250)
                   .HasColumnName("name");

            builder.Property(p => p.description)
                   .HasMaxLength(1000)
                   .HasColumnName("description");

            builder.Property(p => p.price)
                   .HasColumnName("price")
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.active)
                   .HasColumnName("active")
                   .HasDefaultValueSql("((1))");

            builder.Property(p => p.create)
                   .HasColumnName("create")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.update)
                   .HasColumnName("update")
                   .HasDefaultValueSql("GETDATE()");

            // Many-to-many explícito sin entidad intermedia CLR
            builder
                .HasMany(p => p.SalesPoints)
                .WithMany(s => s.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "product_salespoints",
                    j => j
                        .HasOne<SalesPoint>()
                        .WithMany()
                        .HasForeignKey("salespoint_id")
                        .HasConstraintName("fk_productsales_salespoint")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Product>()
                        .WithMany()
                        .HasForeignKey("product_id")
                        .HasConstraintName("fk_productsales_product")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.ToTable("product_salespoints", "sales");
                        j.HasKey("product_id", "salespoint_id");
                        j.Property<int>("product_id").HasColumnName("product_id");
                        j.Property<int>("salespoint_id").HasColumnName("salespoint_id");
                    });
        }
    }
}