namespace FullCart.Domain.Entities
{
    public class OrderDetail : BaseAuditableEntity
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
