﻿namespace FullCart.Domain.Entities;

public partial class UserPermission : BaseAuditableEntity
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int PermissionId { get; set; }
    public virtual Permission Permission { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
