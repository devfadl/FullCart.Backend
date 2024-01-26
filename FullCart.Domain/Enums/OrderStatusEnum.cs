using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Enums
{
    public enum OrderStatusEnum
    {
        New =1,
        Pending=2,
        Shipped=3,
        Delivered=4,
        Retrieved=5,
        Canceled=6
    }
}
