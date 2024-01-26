namespace FullCart.Domain.Entities;

public partial class GroupPermission : BaseAuditableEntity
{
    public int Id { get; set; }
    public Guid GroupId { get; set; }
    public int PermissionId { get; set; }
    public virtual Group Group { get; set; } = null!;
    public virtual Permission Permission { get; set; } = null!;
}
