namespace FullCart.Domain.Entities;

public partial class Permission
{
    public Permission()
    {
        GroupPermissions = new HashSet<GroupPermission>();
        UserPermissions = new HashSet<UserPermission>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public bool IsDeleted { get; set; }

    public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    public virtual ICollection<UserPermission> UserPermissions { get; set; }
}
