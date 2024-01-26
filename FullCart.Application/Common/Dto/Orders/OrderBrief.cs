using FullCart.Application.Common.Dto.Customers;
using FullCart.Application.Common.Mappings;
using FullCart.Domain.Entities;

namespace FullCart.Application.Common.Dto.Orders
{
    public record OrderBrief : IMapFrom<Order>
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual CustomerBrief Customer { get; set; }
        public virtual OrderStatusLookupBrief OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal VATRatio { get; set; }
        public decimal TotalVAT { get; set; }
        public decimal TotalPriceWithVAT { get; set; }
        public List<OrderDetailBrief> OrderDetails { get; set; }
    }
}
