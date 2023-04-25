namespace ICS.Auth0;

/// <summary>
/// 
/// </summary>
public interface IWebHookApplicationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="email"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Guid> PreRegistration(Guid userId, string email, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="icsUserId"></param>
    /// <param name="providerIdentity"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task PostRegistration(Guid icsUserId, string providerIdentity, CancellationToken token);
}

/// <inheritdoc cref="IWebHookApplicationService"/>
public class WebHookApplicationService : IWebHookApplicationService
{
    private readonly IAuth0Repository _auth0Repository;
    private readonly IMessageSession _messageSession;
    private readonly IAuthenticationClient _authenticationClient;
    private readonly IManagementClient _managementClient;
    private readonly ILogger<WebHookApplicationService> _logger;

    public WebHookApplicationService(
        IAuth0Repository auth0Repository,
        IMessageSession messageSession,
        IManagementClient managementClient,
        IAuthenticationClient authenticationClient,
        ILogger<WebHookApplicationService> logger)
    {
        _auth0Repository = auth0Repository;
        _messageSession = messageSession;
        _managementClient = managementClient;
        _authenticationClient = authenticationClient;
        _logger = logger;
    }

    public async Task<Guid> PreRegistration(Guid userId, string email, CancellationToken token)
    {
        // Create a new user
        var user = new User(userId, email, DateTime.UtcNow);

        // A different UserId will be returned if the email was found in the database
        user = await _auth0Repository.AddNewUser(user, token).ConfigureAwait(false);

        // Notify the system about the new user
        var userRegistered = new UserRegistered(user.UserId);

        await _messageSession.Publish(userRegistered).ConfigureAwait(false);

        return user.UserId;
    }

    public async Task PostRegistration(Guid icsUserId, string providerIdentity, CancellationToken token)
    {
        var identity = new Identity(icsUserId, providerIdentity, DateTime.UtcNow);

        var authToken = await _authenticationClient.GetToken(token).ConfigureAwait(false);
        await _managementClient.AddDefaultRoles(authToken.IdToken, providerIdentity, token).ConfigureAwait(false);
        await _auth0Repository.ActivateNewIdentity(identity, token).ConfigureAwait(false);
    }
}