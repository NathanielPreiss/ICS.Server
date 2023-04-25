namespace ICS.Workout;

/// <summary>
/// Exposes handlers for domain events.
/// </summary>
public interface IHandlerApplicationService
{
    /// <summary>
    /// Process new users
    /// </summary>
    /// <param name="userId">A new user id that has been added.</param>
    /// <param name="token">Cancellation token support.</param>
    public Task RegisterUser(Guid userId, CancellationToken token);
}

/// <inheritdoc cref="IWorkoutApplicationService"/>
public class HandlerApplicationService : IHandlerApplicationService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<HandlerApplicationService> _logger;

    public HandlerApplicationService(
        IUserRepository userRepository,
        ILogger<HandlerApplicationService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task RegisterUser(Guid userId, CancellationToken token)
    {
        try
        {
            var user = new User(userId);

            await _userRepository
                .AddNewUser(user, token)
                .ConfigureAwait(false);
        }
        catch (UniqueConstraintViolationException ex)
        {
            // Consume the exception because we only care about the id.
            _logger.LogWarning(ex, "User was previously registered to domain.");
        }
    }
}
