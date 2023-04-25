namespace ICS.Api;

public class AdminRequirementHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.User.IsInRole("Admin"))
        {
            context.PendingRequirements.ToList().ForEach(context.Succeed);
        }

        return Task.CompletedTask;
    }
}
