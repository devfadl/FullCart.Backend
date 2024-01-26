using FullCart.Application.Common.Mappings;

namespace FullCart.Application.Common.Dto;

public class UserGroupBriefDto : IMapFrom<Domain.Entities.UserGroup>
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public virtual GroupDto Group { get; set; } = null!;
}
