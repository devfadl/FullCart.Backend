using FullCart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FullCart.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Group> Groups { get; }
    DbSet<GroupPermission> GroupPermissions { get; }
    DbSet<Domain.Entities.Permission> Permissions { get; }
    DbSet<Domain.Entities.User> Users { get; }
    DbSet<UserGroup> UserGroups { get; }
    DbSet<UserPermission> UserPermissions { get; }
    DbSet<TransactionType> TransactionType { get; }
    DbSet<UserTransaction> UserTransaction { get; }
    DbSet<Product> Product { get; }
    DbSet<ProductCategory> ProductCategory { get; }
    DbSet<Brand> Brand { get; }
    DbSet<ProductImage> ProductImage { get; }
    DbSet<Order> Order { get; }
    DbSet<OrderDetail> OrderDetail { get; }
    DbSet<OrderStatusLookup> OrderStatusLookup { get; }
    DbSet<Inventory> Inventory { get; }
    DbSet<InventoryItem> InventoryItem { get; }
    DbSet<Customer> Customer { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    public DatabaseFacade ContextDataBase();
}
