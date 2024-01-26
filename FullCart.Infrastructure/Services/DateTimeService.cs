using FullCart.Application.Common.Interfaces;

namespace FullCart.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
