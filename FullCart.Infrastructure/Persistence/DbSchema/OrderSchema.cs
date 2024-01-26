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
    public class OrderSchema : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.OrderNumber).HasMaxLength(10);

            builder.HasIndex(x => new { x.OrderNumber }).IsUnique();


            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(0);

            builder.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.TotalPrice).HasColumnType("decimale").HasDefaultValueSql("(decimal(18,2))");

            builder.Property(e => e.VATRatio).HasColumnType("decimale").HasDefaultValueSql("(decimal(18,2))");

            builder.Property(e => e.TotalPriceWithVAT).HasColumnType("decimale").HasDefaultValueSql("(decimal(18,2))");

            builder.Property(e => e.TotalVAT).HasColumnType("decimale").HasDefaultValueSql("(decimal(18,2))");

            builder.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.LastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.HasOne(d => d.OrderStatus)
                 .WithMany()
                 .HasForeignKey(d => d.OrderStatusId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Customer)
                 .WithMany()
                 .HasForeignKey(d => d.CustomerId)
                 .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}