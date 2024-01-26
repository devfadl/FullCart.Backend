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
    public class OrderStatusLookupSchema : IEntityTypeConfiguration<OrderStatusLookup>
    {
        public void Configure(EntityTypeBuilder<OrderStatusLookup> builder)
        {
            builder.ToTable("OrderStatusLookup");

            builder.HasData(
              new TransactionType() { Id = 1, IsDeleted = false, Name = "New" },
              new TransactionType() { Id = 2, IsDeleted = false, Name = "Pending" },
              new TransactionType() { Id = 3, IsDeleted = false, Name = "Shipped" },
              new TransactionType() { Id = 4, IsDeleted = false, Name = "Delivered" },
              new TransactionType() { Id = 5, IsDeleted = false, Name = "Retrieved" },
              new TransactionType() { Id = 6, IsDeleted = false, Name = "Canceled" }
              );

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(50);
        }
    }
}