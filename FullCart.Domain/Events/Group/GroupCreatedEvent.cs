namespace FullCart.Domain.Events;

public class GroupCreatedEvent : BaseEvent
{
    public GroupCreatedEvent(Entities.Group group)
    {
        Group = group;
    }

    public Entities.Group Group { get; }
}

