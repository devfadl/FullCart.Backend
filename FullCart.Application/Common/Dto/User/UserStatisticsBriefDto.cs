using FullCart.Application.Common.Mappings;

namespace FullCart.Application.Common.Dto;
public class UserStatisticsBriefDto : IMapFrom<Domain.Entities.User>
{
    public int TotalUsers { get; set; }
    public int ActiveUsers { get; set; }
    public int InActiveUsers { get; set; }
}