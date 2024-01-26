using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FullCart.Infrastructure.DbSchema;
public class GroupPermissionSchema : IEntityTypeConfiguration<GroupPermission>
{
    public void Configure(EntityTypeBuilder<GroupPermission> builder)
    {

        builder.ToTable("GroupPermission");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Created)
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("(getdate())");

        builder.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                  .IsRequired()
                .IsUnicode(false);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(0);

        builder.Property(e => e.LastModified).HasColumnType("datetime");

        builder.Property(e => e.LastModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

        builder.HasOne(d => d.Group)
                .WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupPermission_Group");

        builder.HasOne(d => d.Permission)
                .WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupPermission_Permission");
    }
}
