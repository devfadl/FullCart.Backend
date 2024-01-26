using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Infrastructure.Persistence.DbSchema
{
    public class ProductSchema : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Description).HasMaxLength(500);

            builder.Property(e => e.Name)
            .HasMaxLength(250)
            .IsRequired();


            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(0);

            builder.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.LastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.HasOne(d => d.ProductCategory)
                 .WithMany()
                 .HasForeignKey(d => d.ProductCategoryId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Product_Category");

            builder.HasOne(d => d.Brand)
                 .WithMany()
                 .HasForeignKey(d => d.BrandId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Product_Brand");

        }
    }
}