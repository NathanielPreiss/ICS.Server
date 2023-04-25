
namespace ICS.Muscle;

/// <summary>
/// 
/// </summary>
public class MemberRequirement : IAuthorizationRequirement
{
}

/// <summary>
/// 
/// </summary>
public class MemberRequirementHandler : AuthorizationHandler<MemberRequirement>
{
    private readonly ILogger<MemberRequirementHandler> _logger;

    public MemberRequirementHandler(
        ILogger<MemberRequirementHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MemberRequirement requirement)
    {
        if (!context.User.IsInRole(Constants.AuthRoles.Member))
        {
            return Task.CompletedTask;
        }

        _logger.LogDebug("Success");

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}