using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Infrastructure.Persistence.DbSchema
{
    public class BrandSchema : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {

            builder.ToTable("Brand");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(0);

            builder.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.LastModified).HasColumnType("datetime");

            builder.Property(e => e.LastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

        }
    }
}