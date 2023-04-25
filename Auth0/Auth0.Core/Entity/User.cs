namespace ICS.Auth0;

public class User
{
    public User(Guid userId, string email, DateTime createdUtc)
    {
        UserId = userId;
        Email = email;
        CreatedUtc = createdUtc;
    }

    [JsonConstructor]
    private User()
        : this(Guid.Empty, string.Empty, DateTime.MinValue)
    {
    }

    public Guid UserId { get; set; }
    public string Email { get; set; }
    public DateTime CreatedUtc { get; set; }

    public ICollection<Identity>? Identities { get; set; }
}
