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

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Code)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("code");

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(250)
                   .HasColumnName("name");

            builder.Property(p => p.Description)
                   .HasMaxLength(1000)
                   .HasColumnName("description");

            builder.Property(p => p.Price)
                   .HasColumnName("price")
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Active)
                   .HasColumnName("active")
                   .HasDefaultValueSql("((1))");

            builder.Property(p => p.Create)
                   .HasColumnName("create")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.Update)
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