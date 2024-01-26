using FullCart.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Persistence.DbSchema;

public class InventoryItemSchema : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItem");

        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.CostPrice).HasColumnType("decimale").HasDefaultValueSql("(decimal(18,2))").IsRequired(false);

        builder.Property(e => e.ExpirationDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())").IsRequired(false);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(0);

        builder.Property(e => e.Created)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(e => e.LastModifiedBy)
       .HasMaxLength(50)
       .IsUnicode(false);

        builder.Property(e => e.LastModified).HasColumnType("datetime");

        builder.HasOne(d => d.Inventory)
             .WithMany(a => a.InventoryItems)
             .HasForeignKey(d => d.InventoryId)
             .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Product).WithMany().HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.ClientSetNull);

    }
}