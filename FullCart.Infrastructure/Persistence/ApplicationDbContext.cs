using FullCart.Application.Common.Interfaces;
using FullCart.Domain.Entities;
using FullCart.Infrastructure.Persistence.Interceptors;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System.Reflection;


namespace FullCart.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;


    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public virtual DbSet<Group> Groups { get; set; } = null!;
    public virtual DbSet<GroupPermission> GroupPermissions { get; set; } = null!;
    public virtual DbSet<Permission> Permissions { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;
    public virtual DbSet<UserPermission> UserPermissions { get; set; } = null!;
    public virtual DbSet<TransactionType> TransactionType { get; set; } = null!;
    public virtual DbSet<UserTransaction> UserTransaction { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<Group>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<GroupPermission>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<Permission>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<UserGroup>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<UserPermission>().HasQueryFilter(p => !p.IsDeleted);
        //OnModelCreatingPartial(builder);

        base.OnModelCreating(builder);


    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public DatabaseFacade ContextDataBase()
    {
        return base.Database;
    }

}
