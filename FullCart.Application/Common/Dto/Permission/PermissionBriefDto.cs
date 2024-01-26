using FullCart.Application.Common.Mappings;

namespace FullCart.Application.Common.Dto;

public class PermissionBriefDto : IMapFrom<Domain.Entities.Permission>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
