namespace FullCart.Domain.Entities;
public class UserTransaction : BaseAuditableEntity
{
    public long Id { get; set; }
    public long TransactionTypeId { get; set; }
    public string ObjectId { get; set; }
    public string ObjectData { get; set; }
    public string? ParentObjectId { get; set; }
    public User CreatedByUser { get; set; }
    public TransactionType TransactionType { get; set; }

}
