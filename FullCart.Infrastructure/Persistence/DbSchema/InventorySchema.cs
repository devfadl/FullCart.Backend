using FullCart.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Persistence.DbSchema;

public class InventorySchema : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventory");

        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id).ValueGeneratedNever();


        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.Location).HasMaxLength(50);

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
        builder.HasData(
              new Inventory() { Id = 1, IsDeleted = false, Name = "Riyadh Inventory",Location= "Riyadh" }
              );


    }
}