using FullCart.Application.Common.Mappings;
using FullCart.Domain.Entities;

namespace FullCart.Application.Common.Dto.Products;

public record ProductBrief : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public int ProductCategoryId { get; set; }
    public string? SKU { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; }
}
