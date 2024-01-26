namespace FullCart.Domain.Entities;

public partial class UserGroup : BaseAuditableEntity
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public User User { get; set; }
    public Group Group { get; set; }

}
