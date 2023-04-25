namespace ICS.Auth0;

public class PreRegistrationResponse
{
    public PreRegistrationResponse(Guid icsUserId)
    {
        IcsUserId = icsUserId;
    }

    public Guid IcsUserId { get; set; }
}
