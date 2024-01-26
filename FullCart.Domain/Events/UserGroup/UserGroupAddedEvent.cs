namespace FullCart.Domain.Events;

public class UserGroupAddedEvent : BaseEvent
{
    public UserGroupAddedEvent(ICollection<Entities.UserGroup> userGroups)
    {
        UserGroup = userGroups;
    }

    public ICollection<Entities.UserGroup> UserGroup { get; }
}

