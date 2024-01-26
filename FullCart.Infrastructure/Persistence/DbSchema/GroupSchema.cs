using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Infrastructure.DbSchema;

public class GroupSchema : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {

        builder.ToTable("Group");

        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Description).HasMaxLength(200);

        builder.Property(e => e.Name)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(0);

        builder.Property(e => e.Created)
            .HasColumnType("datetime")
            .IsRequired()
            .HasDefaultValueSql("(getdate())");

        builder.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

        builder.Property(e => e.LastModified).HasColumnType("datetime");

        builder.Property(e => e.LastModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
    }
}
