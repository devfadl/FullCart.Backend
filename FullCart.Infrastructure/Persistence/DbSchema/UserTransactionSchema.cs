using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Infrastructure.DbSchema;

public class UserTransactionSchema : IEntityTypeConfiguration<UserTransaction>
{
    public void Configure(EntityTypeBuilder<UserTransaction> builder)
    {
        builder.ToTable("UserTransaction");

        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id);

        builder.Property(e => e.TransactionTypeId);

        builder.Property(e => e.ObjectId).HasMaxLength(3000);

        builder.Property(e => e.ParentObjectId).HasMaxLength(50);

        builder.Property(e => e.ObjectData);

        builder.Property(k => k.Created)
            .HasColumnType("datetime")
            .IsRequired()
            .HasDefaultValueSql("(getdate())");

        builder.Property(k => k.LastModified)
            .HasColumnType("datetime");

        builder.Property(k => k.LastModifiedBy)
            .HasMaxLength(50);

        builder.Property(k => k.IsDeleted)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasOne(k => k.CreatedByUser)
            .WithMany(k => k.UserTransactions)
            .HasForeignKey(k => k.CreatedBy);

        builder.HasOne(k => k.TransactionType)
            .WithMany(k => k.UserTransactions)
            .HasForeignKey(k => k.TransactionTypeId);
    }
}
