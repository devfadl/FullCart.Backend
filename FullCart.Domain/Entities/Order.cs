namespace FullCart.Domain.Entities
{
    public class Order: BaseAuditableEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer  { get; set; }
        public int OrderStatusId { get; set; }
        public virtual OrderStatusLookup OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal VATRatio { get; set; }
        public decimal TotalVAT { get; set; }
        public decimal TotalPriceWithVAT { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
