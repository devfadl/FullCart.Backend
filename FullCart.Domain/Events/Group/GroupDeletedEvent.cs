namespace FullCart.Domain.Events;

public class GroupDeletedEvent : BaseEvent
{
    public GroupDeletedEvent(Entities.Group group)
    {
        Group = group;
    }

    public Entities.Group Group { get; }
}

