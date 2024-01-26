using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Infrastructure.DbSchema;

public class PermissionSchema : IEntityTypeConfiguration<Permission>
{

    public void Configure(EntityTypeBuilder<Permission> builder)
    {

        builder.ToTable("Permission");
        builder.HasData(
        new Permission() { Id = 1, IsDeleted = false, Name = "Admininistrator", Code = "Admininistrator" },
        new Permission() { Id = 2, IsDeleted = false, Name = "Customer", Code = "Customer" }
        );
        builder.HasKey(p => p.Id); builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(100)
              .IsRequired();
        builder.Property(e => e.Code)
         .HasMaxLength(100)
           .IsRequired();
    }
}
