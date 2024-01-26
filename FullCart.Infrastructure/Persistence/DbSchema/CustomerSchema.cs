using FullCart.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Persistence.DbSchema
{
    public class CustomerSchema : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();


            builder.Property(e => e.Address).HasMaxLength(30);
            builder.Property(e => e.TaxNumber).HasMaxLength(14).IsRequired(false);


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
}
