using FullCart.Application.Common.Mappings;

namespace FullCart.Application.Common.Dto;

public class GroupBriefDto : IMapFrom<Domain.Entities.Group>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public int UsersCount { get; set; }
    public List<GroupPermissionBriefDto> GroupPermissions { get; set; }

}
