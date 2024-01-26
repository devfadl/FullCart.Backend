using FullCart.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Persistence.DbSchema
{
    internal class ProductImageSchema : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {

            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();

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

            builder.HasOne(d => d.Product)
                 .WithMany()
                 .HasForeignKey(d => d.ProductId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Product_Image");
        }
    }
}