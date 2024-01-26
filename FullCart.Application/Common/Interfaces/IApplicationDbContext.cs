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

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    public DatabaseFacade ContextDataBase();
}
