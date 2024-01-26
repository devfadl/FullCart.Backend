namespace FullCart.Domain.Entities;

public partial class Group : BaseAuditableEntity
{
    public Group()
    {
        GroupPermissions = new HashSet<GroupPermission>();
        UserGroups = new HashSet<UserGroup>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    public virtual ICollection<UserGroup> UserGroups { get; set; }

}
