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
    public class OrderDetailSchema : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {

            builder.ToTable("OrderDetail");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();


            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(0);

            builder.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.TotalPrice).HasColumnType("decimale").HasDefaultValueSql("(decimal(18,2))");

            builder.Property(e => e.Price).HasColumnType("decimale").HasDefaultValueSql("(decimal(18,2))");

            builder.Property(e => e.Quantity).HasColumnType("decimale").HasDefaultValueSql("(decimal(18,2))");

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
                 .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Order)
                 .WithMany(a=>a.OrderDetails)
                 .HasForeignKey(d => d.OrderId)
                 .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}