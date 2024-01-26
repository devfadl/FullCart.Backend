using FullCart.Application.Common.Mappings;

namespace FullCart.Application.Common.Dto;

public class GroupPermissionBriefDto : IMapFrom<Domain.Entities.GroupPermission>
{
    public int Id { get; set; }
    public Guid GroupId { get; set; }
    public int PermissionId { get; set; }
    public virtual PermissionBriefDto Permission { get; set; } = null!;
}
