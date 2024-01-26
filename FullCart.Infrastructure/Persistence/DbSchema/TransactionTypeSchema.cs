using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Infrastructure.DbSchema;

public class TransactionTypeSchema : IEntityTypeConfiguration<TransactionType>
{
    public void Configure(EntityTypeBuilder<TransactionType> builder)
    {
        builder.ToTable("TransactionType");

        builder.HasData(
          new TransactionType() { Id = 1, IsDeleted = false, Name  = "UserCreated" },
          new TransactionType() { Id = 2, IsDeleted = false, Name  = "UserUpdated" },
          new TransactionType() { Id = 3, IsDeleted = false, Name  = "GroupCreated" },
          new TransactionType() { Id = 4, IsDeleted = false, Name  = "GroupDeleted" },
          new TransactionType() { Id = 5, IsDeleted = false, Name  = "GroupUpdated" },
          new TransactionType() { Id = 6, IsDeleted = false, Name  = "UserGroupAdded" },
          new TransactionType() { Id = 7, IsDeleted = false, Name  = "PermissionGroupAdded" },
          new TransactionType() { Id = 8, IsDeleted = false, Name  = "ProductAdded" },
          new TransactionType() { Id = 9, IsDeleted = false, Name  = "ProductDeleted" },
          new TransactionType() { Id = 10, IsDeleted = false, Name = "ProductUpdated" },
          new TransactionType() { Id = 11, IsDeleted = false, Name = "ProductCategoryAdded" },
          new TransactionType() { Id = 12, IsDeleted = false, Name = "ProductCategoryDeleted" },
          new TransactionType() { Id =13, IsDeleted = false, Name  = "ProductCategoryUpdated" },
          new TransactionType() { Id = 14, IsDeleted = false, Name = "BrandAdded" },
          new TransactionType() { Id = 15, IsDeleted = false, Name = "BrandDeleted" },
          new TransactionType() { Id = 16, IsDeleted = false, Name = "BrandUpdated" }
          );

        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(50);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(0);
    }
}