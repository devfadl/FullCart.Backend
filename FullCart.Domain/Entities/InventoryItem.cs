using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Entities
{
    public class InventoryItem:BaseAuditableEntity
    {
        public int Id { get; set; }
        public int InventoryId { get; set; } 
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } 
        public DateTime? ExpirationDate { get; set; } 
        public decimal? CostPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
