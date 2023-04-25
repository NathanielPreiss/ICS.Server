namespace ICS.Workout;

/// <summary>
/// The user database repository
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="user">A user to add.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The final state of the new user.</returns>
    /// <exception cref="UniqueConstraintViolationException" />
    public Task<User> AddNewUser(User user, CancellationToken token);

    /// <summary>
    /// Returns all workouts for a specific user.
    /// </summary>
    /// <param name="userId">The userId to search for.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>A list of workouts without child objects.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<ICollection<Workout>> GetWorkouts(Guid userId, CancellationToken token);
}

/// <inheritdoc cref="IUserRepository"/>>
public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<WorkoutDbContext> _dbContextFactory;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(
        IDbContextFactory<WorkoutDbContext> dbContextFactory,
        ILogger<UserRepository> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task<User> AddNewUser(User user, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        try
        {
            var userEntity = await dbContext.User
                .AddAsync(user, token)
                .ConfigureAwait(false);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            return userEntity.Entity;
        }
        catch (DbUpdateException ex)
            when (UniqueConstraintViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "User insert failed, user already exists.");
            throw new UniqueConstraintViolationException(nameof(dbContext.User), ex);
        }
    }

    public async Task<ICollection<Workout>> GetWorkouts(Guid userId, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        try
        {
            var user = await dbContext.User
                .Include(x => x.Workouts)
                .SingleAsync(x => x.UserId == userId, token)
                .ConfigureAwait(false);

            var workouts = user.Workouts ?? new List<Workout>()
                .OrderBy(x => x.Position)
                .ToList();

            _logger.LogDebug("User {UserId} had {WorkoutCount} workouts.", userId, workouts.Count);
            return workouts;

        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "User {UserId} not found.", userId);
            throw new EntityNotFoundException(nameof(dbContext.User), ex);
        }
    }
}
