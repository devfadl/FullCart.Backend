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
          new TransactionType() { Id = 1, IsDeleted = false, Name = "إضافة مستخدم" },
          new TransactionType() { Id = 2, IsDeleted = false, Name = "تعديل مستخدم" },
          new TransactionType() { Id = 3, IsDeleted = false, Name = "إضافة مجموعة" },
          new TransactionType() { Id = 4, IsDeleted = false, Name = "حذف مجموعة" },
          new TransactionType() { Id = 5, IsDeleted = false, Name = "تعديل  مجموعة" },
          new TransactionType() { Id = 6, IsDeleted = false, Name = "إضافة مجموعة لمستخدم" },
          new TransactionType() { Id = 7, IsDeleted = false, Name = "إضافة صلاحية لمجموعة" }
          );

        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(50);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(0);
    }
}
