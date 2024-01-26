using FullCart.Application.Common.Mappings;
using FullCart.Domain.Entities;

namespace FullCart.Application.Common.Dto.Products
{
    public record ProductCategoryBrief : IMapFrom<ProductCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
