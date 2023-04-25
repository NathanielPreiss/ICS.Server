namespace ICS.Auth0;

public class UserRegistered : IEvent
{
    public UserRegistered(Guid userId)
    {
        UserId = userId;
    }

    [JsonConstructor]
    private UserRegistered() 
        : this(Guid.Empty)
    {
    }

    public Guid UserId { get; set; }
}
