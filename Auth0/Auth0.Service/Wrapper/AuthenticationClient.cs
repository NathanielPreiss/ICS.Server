namespace ICS.Auth0;

public interface IAuthenticationClient
{
    public Task<AccessTokenResponse?> GetToken(CancellationToken token);
}

public class AuthenticationClient : IAuthenticationClient
{
    private const string Uri = "https://ics-dev.us.auth0.com/";
    private const string Audience = $"{Uri}api/v2/";
    private const string ClientId = "sKYbHs3wqCVNUVqnxBmWuMhwZpvXP1Oy";
    private const string ClientSecret = "OrwWIpnKTBTOGaGlYAdtK1_1763oDipA-j1g6ci_F2UL3FTiu18_sKClm2WB9Eyu";
    private const JwtSignatureAlgorithm Algorithm = JwtSignatureAlgorithm.HS256;

    private readonly IAuthenticationApiClient _authenticationApiClient;

    public AuthenticationClient()
    {
        _authenticationApiClient = new AuthenticationApiClient(new Uri(Uri));
    }

    public async Task<AccessTokenResponse?> GetToken(CancellationToken token)
    {
        var request = new ClientCredentialsTokenRequest
        {
            Audience = Audience,
            ClientId = ClientId,
            ClientSecret = ClientSecret,
            SigningAlgorithm = Algorithm
        };
        var authToken = await _authenticationApiClient.GetTokenAsync(request, token).ConfigureAwait(false);

        return authToken;
    }
}