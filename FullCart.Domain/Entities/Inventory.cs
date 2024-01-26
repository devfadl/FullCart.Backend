using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Entities
{
    public class Inventory :BaseAuditableEntity
    {
        public Inventory()
        {
            InventoryItems = new HashSet<InventoryItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set;}
    }
}
