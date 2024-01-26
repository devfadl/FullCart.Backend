using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Infrastructure.DbSchema;

public class UserSchema : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Email).HasMaxLength(50);

        builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

        builder.Property(e => e.FirstName).HasMaxLength(30);

        builder.Property(e => e.SecondName).HasMaxLength(30);

        builder.Property(e => e.ThirdName).HasMaxLength(30);

        builder.Property(e => e.LastName).HasMaxLength(30);

        builder.Property(e => e.PhoneNumber).HasMaxLength(10);

        builder.Property(e => e.Username).HasMaxLength(50);

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

    }
}
