using FullCart.Application.Common.Mappings;
using FullCart.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Common.Dto.Products;

public record ProductsBrief : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public  BrandBrief Brand { get; set; }
    public  ProductCategoryBrief ProductCategory { get; set; }
    public List<ProductImageBrief> ProductImages { get; set; }
    public string? SKU { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; }
}
