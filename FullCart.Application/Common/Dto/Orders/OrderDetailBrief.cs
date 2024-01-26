using FullCart.Application.Common.Dto.Products;
using FullCart.Application.Common.Mappings;
using FullCart.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Common.Dto.Orders
{
    public record OrderDetailBrief:IMapFrom<OrderDetail>
    {
        public int Id { get; set; }
        public ProductBasicBrief Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
