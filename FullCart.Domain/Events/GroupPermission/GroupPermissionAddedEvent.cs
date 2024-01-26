namespace FullCart.Domain.Events;

public class GroupPermissionAddedEvent : BaseEvent
{
    public GroupPermissionAddedEvent(ICollection<Entities.GroupPermission> groupPermission)
    {
        GroupPermission = groupPermission;
    }

    public ICollection<Entities.GroupPermission> GroupPermission { get; }
}

