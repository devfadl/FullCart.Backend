namespace FullCart.Domain.Events;

public class UserUpdatedEvent : BaseEvent
{
    public UserUpdatedEvent(Entities.User user)
    {
        User = user;
    }

    public Entities.User User { get; }
}

