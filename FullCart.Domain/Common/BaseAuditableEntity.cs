namespace FullCart.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }
}
