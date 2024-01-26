namespace FullCart.Domain.Entities
{
    public class ProductImage: BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
