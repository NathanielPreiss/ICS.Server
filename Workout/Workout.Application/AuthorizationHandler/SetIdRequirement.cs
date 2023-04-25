namespace ICS.Workout;

/// <summary>
/// 
/// </summary>
public class SetIdRequirement : IAuthorizationRequirement
{
}
/// <summary>
/// 
/// </summary>
public class SetIdRequirementHandler : AuthorizationHandler<SetIdRequirement>
{
    private readonly IDbContextFactory<WorkoutDbContext> _dbContextFactory;
    private readonly ILogger<SetIdRequirementHandler> _logger;
    private const string RoutineIdKey = "routineId";
    private const string MemberRoleKey = "Member";

    public SetIdRequirementHandler(
        IDbContextFactory<WorkoutDbContext> dbContextFactory,
        ILogger<SetIdRequirementHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SetIdRequirement requirement)
    {
        context.Succeed(requirement);
        return;
        if (!context.User.IsInRole(MemberRoleKey))
        {
            _logger.LogTrace("User is not a Member");
            return;
        }

        if (context.Resource is not DefaultHttpContext httpContext)
        {
            _logger.LogError("Could not get http context from resource.");
            return;
        }
        
        if (!httpContext.Request.RouteValues.TryGetValue(RoutineIdKey, out var routineIdParam))
        {
            _logger.LogError($"Failed to retrieve {RoutineIdKey} from route parameter.");
            return;
        }

        httpContext.Request.Query.TryGetValue("", out var x);
        if (!Guid.TryParse((string?)routineIdParam, out var routineId))
        {
            _logger.LogError($"Failed to determine {RoutineIdKey} from route parameter.");
            return;
        }

        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync()
            .ConfigureAwait(false);

        var userId = await dbContext.Routine
            .Where(x => x.RoutineId == routineId)
            .Include(x => x.Workout)
            .Select(x => x.Workout!.UserId)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (userId == Guid.Empty)
        {
            _logger.LogError("Routine {RoutineId} was not found.", routineId);
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