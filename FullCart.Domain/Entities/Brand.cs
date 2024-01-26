namespace FullCart.Domain.Entities
{
    public class Brand: BaseAuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
