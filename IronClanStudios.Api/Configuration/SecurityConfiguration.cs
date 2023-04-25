namespace ICS.Api;

public class AuthenticationConfiguration
{
    internal const string Section = "Authentication";

    public string Audience { get; set; }
    public string ClientId { get; set; }
    public string Issuer { get; set; }
    public string SigningKey { get; set; }
    public string ClientSecret { get; set; }

    public AuthenticationConfiguration()
    {
        Audience = string.Empty;
        ClientId = string.Empty;
        Issuer = string.Empty;
        SigningKey = string.Empty;
        ClientSecret = string.Empty;
    }
}
