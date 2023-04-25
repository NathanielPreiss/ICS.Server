namespace ICS.Workout;

/// <summary>
/// 
/// </summary>
public class UserIdRequirement : IAuthorizationRequirement
{
}

/// <summary>
/// 
/// </summary>
public class UserIdRequirementHandler : AuthorizationHandler<UserIdRequirement>
{
    private readonly IDbContextFactory<WorkoutDbContext> _dbContextFactory;
    private readonly ILogger<UserIdRequirementHandler> _logger;
    private const string UserIdKey = "userId";

    public UserIdRequirementHandler(
        IDbContextFactory<WorkoutDbContext> dbContextFactory,
        ILogger<UserIdRequirementHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        UserIdRequirement requirement)
    {
        if(!context.User.IsInRole("Member"))
        {
            return;
        }

        if (context.Resource is not DefaultHttpContext httpContext)
        {
            _logger.LogError("Could not get http context from resource.");
            return;
        }

        if (!httpContext.Request.RouteValues.TryGetValue(UserIdKey, out var userIdParam))
        {
            _logger.LogError("Failed to retrieve userId from route parameter.");
            return;
        }

        if (!Guid.TryParse((string?)userIdParam, out var userId))
        {
            _logger.LogError("Failed to determine userId from route parameter.");
            return;
        }

        if (userId == Guid.Empty)
        {
            _logger.LogError("UserId {UserId} was an empty value.", userId);
            return;
        }

        var dbContext = await _dbContextFactory
            .CreateDbContextAsync()
            .ConfigureAwait(false);

        var exists = await dbContext.User
            .AnyAsync(x => x.UserId == userId)
            .ConfigureAwait(false);

        if (!exists)
        {
            _logger.LogError("User does not exist in the database.");
            return;
        }

        var claimUserId = context.User.Claims.SingleOrDefault(x => x.Type == "IcsUserId");

        if (claimUserId == null)
        {
            _logger.LogError("User does not have IcsUserId claim.");
            return;
        }

        if (!Guid.TryParse(claimUserId.Value, out var icsUserId))
        {
            _logger.LogError("Failed to determine IcsUserId claim from user claims.");
            return;
        }

        if (icsUserId != userId)
        {
            _logger.LogError("The user id in the claims does not match the user id attached to the workout id.");
            return;
        }

        _logger.LogDebug("Success");

        context.Succeed(requirement);
    }
}