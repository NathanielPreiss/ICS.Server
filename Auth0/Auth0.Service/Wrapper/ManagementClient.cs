using Auth0User = Auth0.ManagementApi.Models.User;

namespace ICS.Auth0;

public interface IManagementClient
{
    public Task<IEnumerable<Auth0User>> UsersGetAll(string accessToken, CancellationToken token);
    public Task AddMetadata(string accessToken, string userId, AppMetadata metadata, CancellationToken token);
    public Task AddDefaultRoles(string accessToken, string userId, CancellationToken token);
}

public class ManagementClient : IManagementClient
{
    private const string Uri = "https://ics-dev.us.auth0.com/api/v2";

    private readonly IAuthenticationClient _authenticationClient;

    public ManagementClient(IAuthenticationClient authenticationClient)
    {
        _authenticationClient = authenticationClient;
    }

    public async Task<IEnumerable<Auth0User>> UsersGetAll(string accessToken, CancellationToken token)
    {
        var managementApiClient = new ManagementApiClient(accessToken, new Uri(Uri));

        var request = new GetUsersRequest();

        var paginationInfo = new PaginationInfo(0, 50, true);

        var results = new List<Auth0User>();

        while (true)
        {
            var x = await managementApiClient.Users.GetAllAsync(request);
            var users = await managementApiClient.Users
                .GetAllAsync(request, paginationInfo, token)
                .ConfigureAwait(false);

            results.AddRange(users);

            paginationInfo = new PaginationInfo(paginationInfo.PageNo + 1, 50, true);

            var count = users.Paging.Start + users.Paging.Length;

            if (count == users.Paging.Total)
                break;
        }

        return results;
    }

    public async Task AddMetadata(string accessToken, string userId, AppMetadata metadata, CancellationToken token)
    {
        var managementApiClient = new ManagementApiClient(accessToken, new Uri(Uri));

        var request = new UserUpdateRequest
        {
            AppMetadata = metadata
        };

        await managementApiClient.Users.UpdateAsync(userId, request, token).ConfigureAwait(false);
    }

    public async Task AddDefaultRoles(string accessToken, string userId, CancellationToken token)
    {
        var managementApiClient = new ManagementApiClient(accessToken, new Uri(Uri));

        var userRoles = await managementApiClient.Users.GetRolesAsync(userId, cancellationToken: token).ConfigureAwait(false);

        if (userRoles.All(x => x.Name != "Admin"))
        {
            var request = new AssignRolesRequest
            {
                Roles = new[] {"Admin"}
            };

            await managementApiClient.Users.AssignRolesAsync(userId, request, token)
                .ConfigureAwait(false);
        }
    }


    /*public async Task<IEnumerable<Auth0User>> AddUserId(string accessToken, CancellationToken token)
    {
        var managementApiClient = new ManagementApiClient(accessToken, new Uri(Uri));
        new UserUpdateRequest()
        {
            
        }
        managementApiClient.Users.UpdateAsync()
    }*/
}