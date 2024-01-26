namespace FullCart.Domain.Entities
{
    public class ProductCategory: BaseAuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
