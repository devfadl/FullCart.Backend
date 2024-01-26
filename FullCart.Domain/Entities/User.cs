namespace FullCart.Domain.Entities;

public class User : BaseAuditableEntity
{
    public User()
    {
        UserPermissions = new HashSet<UserPermission>();
        UserGroups = new HashSet<UserGroup>();
    }

    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string ThirdName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; }
    public virtual ICollection<UserPermission> UserPermissions { get; set; }
    public virtual ICollection<UserGroup> UserGroups { get; set; }
    public virtual ICollection<UserTransaction> UserTransactions { get; set; }

}

