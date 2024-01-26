namespace FullCart.Domain.Events;

public class GroupUpdatedEvent : BaseEvent
{
    public GroupUpdatedEvent(Entities.Group group)
    {
        Group = group;
    }

    public Entities.Group Group { get; }
}

