using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Infrastructure.DbSchema;

public class UserPermissionSchema : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {

        builder.ToTable("UserPermission");

        builder.HasKey(p => p.Id);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(0);

        builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

        builder.Property(e => e.CreatedBy)
              .HasMaxLength(50)
                .IsRequired()
              .IsUnicode(false);

        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(e => e.LastModified).HasColumnType("datetime");

        builder.HasOne(d => d.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermission_Permission");

        builder.HasOne(d => d.User)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermission_User");

    }
}
