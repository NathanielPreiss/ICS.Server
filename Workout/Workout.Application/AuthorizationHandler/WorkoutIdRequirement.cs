namespace ICS.Workout;

/// <summary>
/// 
/// </summary>
public class WorkoutIdRequirement : IAuthorizationRequirement
{
}

/// <summary>
/// 
/// </summary>
public class WorkoutIdRequirementHandler : AuthorizationHandler<WorkoutIdRequirement>
{
    private readonly IDbContextFactory<WorkoutDbContext> _dbContextFactory;
    private readonly ILogger<WorkoutIdRequirementHandler> _logger;
    private const string WorkoutIdKey = "workoutId";

    public WorkoutIdRequirementHandler(
        IDbContextFactory<WorkoutDbContext> dbContextFactory,
        ILogger<WorkoutIdRequirementHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        WorkoutIdRequirement requirement)
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

        if (!httpContext.Request.RouteValues.TryGetValue(WorkoutIdKey, out var workoutIdParam))
        {
            _logger.LogError("Failed to retrieve workoutId from route parameter.");
            return;
        }

        if (!Guid.TryParse((string?)workoutIdParam, out var workoutId))
        {
            _logger.LogError("Failed to determine workoutId from route parameter.");
            return;
        }

        var dbContext = await _dbContextFactory
            .CreateDbContextAsync()
            .ConfigureAwait(false);

        var userId = await dbContext.Workout
            .Where(x => x.WorkoutId == workoutId)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (userId == Guid.Empty)
        {
            _logger.LogError("Workout {WorkoutId} was not found.", workoutId);
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