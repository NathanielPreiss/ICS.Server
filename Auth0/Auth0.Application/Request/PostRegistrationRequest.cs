namespace ICS.Auth0;

public class PostRegistrationRequest
{
    public Guid IcsUserId { get; set; }
    public string ProviderIdentity { get; set; }
}
