namespace FullCart.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual List<ProductImage> ProductImages { get; set; }
        public string? SKU { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
    }
}
