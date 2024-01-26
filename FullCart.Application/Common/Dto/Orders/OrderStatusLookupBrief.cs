using FullCart.Application.Common.Mappings;
using FullCart.Domain.Entities;

namespace FullCart.Application.Common.Dto.Orders
{
    public record OrderStatusLookupBrief :IMapFrom<OrderStatusLookup>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
