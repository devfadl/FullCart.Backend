using FullCart.Application.Common.Mappings;
using FullCart.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Common.Dto.Customers
{
    public record CustomerBrief :IMapFrom<Customer>
    {
        public Guid Id { get; set; }
        public UserBrief User { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
    }
}
