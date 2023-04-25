namespace ICS.Auth0;

/// <summary>
/// 
/// </summary>
public interface IUtilityService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task ImportAllUsers(CancellationToken token);
}

/// <inheritdoc cref="IUtilityService"/>
public class UtilityService : IUtilityService
{
    private readonly IAuthenticationClient _authenticationClient;
    private readonly IManagementClient _managementClient;
    private readonly IWebHookApplicationService _webHookApplicationService;
    private readonly ILogger<UtilityService> _logger;

    public UtilityService(
        IAuthenticationClient authenticationClient,
        IManagementClient managementClient,
        IWebHookApplicationService webHookApplicationService,
        ILogger<UtilityService> logger)
    {
        _authenticationClient = authenticationClient;
        _managementClient = managementClient;
        _webHookApplicationService = webHookApplicationService;
        _logger = logger;
    }

    public async Task ImportAllUsers(CancellationToken token)
    {
        // Create token for Auth0 calls
        var authToken = await _authenticationClient.GetToken(token).ConfigureAwait(false);

        if (authToken == null)
        {
            throw new NullReferenceException();
        }
        
        // Get all the available Auth0 users
        var users = await _managementClient.UsersGetAll(authToken.AccessToken, token).ConfigureAwait(false);

        foreach (var user in users)
        {
            // If the metadata is missing, the web hooks are likely disabled
            var metadata = ((JObject?)user.AppMetadata)?.ToObject<AppMetadata>() ?? new AppMetadata();

            // Update Auth0 with a new IcsUserId
            if (metadata.IcsUserId == Guid.Empty)
            {
                metadata.IcsUserId = Guid.NewGuid();
                await _managementClient.AddMetadata(authToken.AccessToken, user.UserId, metadata, token).ConfigureAwait(false);
            }

            // Run the normal pre registration for the user
            await _webHookApplicationService.PreRegistration(metadata.IcsUserId, user.Email, token).ConfigureAwait(false);

            // Run the post for the current identity
            await _webHookApplicationService.PostRegistration(metadata.IcsUserId, user.UserId, token).ConfigureAwait(false);
        }
    }
}
