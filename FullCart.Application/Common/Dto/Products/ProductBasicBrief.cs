using FullCart.Application.Common.Mappings;
using FullCart.Domain.Entities;

namespace FullCart.Application.Common.Dto.Products;
public record ProductBasicBrief : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? SKU { get; set; }
}