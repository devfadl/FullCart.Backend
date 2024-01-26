namespace FullCart.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }

        //we can add here extra info for customer that we dont need to add in user table
    }
}
