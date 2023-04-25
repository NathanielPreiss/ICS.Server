namespace ICS.Workout;

public class UserRegisteredHandler : IHandleMessages<UserRegistered>
{
    private readonly IHandlerApplicationService _handlerApplicationService;
    private readonly ILogger<UserRegisteredHandler> _logger;

    public UserRegisteredHandler(
        IHandlerApplicationService handlerApplicationService,
        ILogger<UserRegisteredHandler> logger)
    {
        _handlerApplicationService = handlerApplicationService;
        _logger = logger;
    }

    public async Task Handle(UserRegistered message, IMessageHandlerContext context)
    {
        _logger.BeginScope(new
        {
            message.UserId
        });

        await _handlerApplicationService
            .RegisterUser(message.UserId, CancellationToken.None)
            .ConfigureAwait(false);
    }
}
