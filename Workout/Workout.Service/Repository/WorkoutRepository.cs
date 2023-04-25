namespace ICS.Workout;

/// <summary>
/// The workouts repository.
/// </summary>
public interface IWorkoutRepository
{
    /// <summary>
    /// Deletes a workout and its child routine/workout then fixes workout positions.
    /// </summary>
    /// <param name="workoutId">The workout id being deleted.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>An updated current list of workouts.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<ICollection<Workout>> DeleteWorkout(Guid workoutId, CancellationToken token);

    /// <summary>
    /// Returns a workout including all child objects.
    /// </summary>
    /// <param name="workoutId">The workout id being searched for.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>A single workout with all child properties.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<Workout> GetWorkout(Guid workoutId, CancellationToken token);

    /// <summary>
    /// Gets all of a users workouts.
    /// </summary>
    /// <param name="userId">The user to get the workouts for.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>A all workouts for a single user.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<ICollection<Workout>> GetWorkouts(Guid userId, CancellationToken token);

    /// <summary>
    /// Move a single workout to a new position.
    /// </summary>
    /// <param name="workoutId">The workout id being moved.</param>
    /// <param name="position">The new position of the workout.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>An update list of workouts at their new positions.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<ICollection<Workout>> MoveWorkout(Guid workoutId, int position, CancellationToken token);

    /// <summary>
    /// Patches the writable fields for a workout.
    /// </summary>
    /// <param name="workoutId">The id of the workout being updated.</param>
    /// <param name="name">A new name for the workout.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The updated workout.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<Workout> PatchWorkout(Guid workoutId, string name, CancellationToken token);

    /// <summary>
    /// Adds a new workout to the database.
    /// </summary>
    /// <param name="workout">A workout to add to the database.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The final workout object added to the database.</returns>
    /// <exception cref="UniqueConstraintViolationException" />
    /// <exception cref="ForeignKeyViolationException" />
    public Task<Workout> PostWorkout(Workout workout, CancellationToken token);
}

/// <inheritdoc cref="IWorkoutRepository" />
public class WorkoutRepository : IWorkoutRepository
{
    private readonly IDbContextFactory<WorkoutDbContext> _dbContextFactory;
    private readonly ILogger<WorkoutRepository> _logger;

    public WorkoutRepository(
        IDbContextFactory<WorkoutDbContext> dbContextFactory,
        ILogger<WorkoutRepository> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task<ICollection<Workout>> DeleteWorkout(Guid workoutId, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        // Transaction needed because of the multiple tables being updated
        var transaction = await dbContext.Database
            .BeginTransactionAsync(token)
            .ConfigureAwait(false);

        try
        {
            // Confirm the user exists
            var user = await dbContext.User
                .Include(x => x.Workouts)
                .Where(x => x.Workouts!.Any(y => y.WorkoutId == workoutId))
                .SingleAsync(token)
                .ConfigureAwait(false);

            var workouts = user.Workouts!
                .OrderBy(x => x.Position)
                .ToList();

            // Confirm the workout exists.
            var workout = workouts.Single(x => x.WorkoutId == workoutId);

            // Get list of routines to delete
            var routines = await dbContext.Routine
                .Include(x => x.Sets)
                .Where(x => x.WorkoutId == workoutId)
                .ToListAsync(token)
                .ConfigureAwait(false);

            // Get list of sets to delete
            var sets = routines
                .SelectMany(x => x.Sets ?? new List<Set>());

            // Remove the records
            dbContext.Set.RemoveRange(sets);
            dbContext.Routine.RemoveRange(routines);
            dbContext.Workout.Remove(workout);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            workouts.Remove(workout);

            // Normalize the position of the remaining workouts
            for (var i = 0; i < workouts.Count; i++)
            {
                workouts[i].Position = i + 1;
            }

            _logger.LogInformation("{Count} remaining workouts.", workouts.Count);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            await transaction
                .CommitAsync(token)
                .ConfigureAwait(false);

            return workouts;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Workout was not found");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new EntityNotFoundException(nameof(dbContext.Workout), ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error happened deleting workout.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }

    public async Task<Workout> GetWorkout(Guid workoutId, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        try
        {
            var workout = await dbContext.Workout
                .Include(x => x.Routines)!
                .ThenInclude(x => x.Sets)
                .SingleAsync(x => x.WorkoutId == workoutId, token)
                .ConfigureAwait(false);

            return workout;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Workout was not found");
            throw new EntityNotFoundException(nameof(dbContext.Workout), ex);
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

            return user.Workouts?.ToList() ?? new List<Workout>();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "User was not found");
            throw new EntityNotFoundException(nameof(dbContext.User), ex);
        }
    }

    public async Task<ICollection<Workout>> MoveWorkout(Guid workoutId, int position, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var transaction = await dbContext.Database
            .BeginTransactionAsync(token)
            .ConfigureAwait(false);

        try
        {
            // Get the user in order to get all the workouts
            var user = await dbContext.User
                .Include(x => x.Workouts)
                .Where(x => x.Workouts!.Any(y => y.WorkoutId == workoutId))
                .SingleAsync(token)
                .ConfigureAwait(false);

            var workouts = user.Workouts!
                .OrderBy(x => x.Position)
                .ToList();

            var workout = workouts.Single(x => x.WorkoutId == workoutId);

            var finalPosition = Math.Min(Math.Max(1, position), workouts.Count);

            // This is to free up the current set id
            workout.Position = -1;

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            // Move the workout to the correct position in the list
            workouts.Remove(workout);
            workouts.Insert(finalPosition - 1, workout);

            // Normalize the position of the workouts
            for (var i = 0; i < workouts.Count; i++)
            {
                workouts[i].Position = i + 1;
            }

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            await transaction
                .CommitAsync(token)
                .ConfigureAwait(false);

            return workouts;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Workout was not found");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new EntityNotFoundException(nameof(dbContext.Workout), ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error happened updating workout position.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }

    public async Task<Workout> PatchWorkout(Guid workoutId, string name, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        try
        {
            var workout = await dbContext.Workout
                .SingleAsync(x => x.WorkoutId == workoutId, token)
                .ConfigureAwait(false);

            // Update the valid fields
            workout.Name = name;

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            return workout;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Workout was not found");
            throw new EntityNotFoundException(nameof(dbContext.Workout), ex);
        }
    }

    public async Task<Workout> PostWorkout(Workout workout, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var transaction = await dbContext.Database
            .BeginTransactionAsync(token)
            .ConfigureAwait(false);

        try
        {
            var user = await dbContext.User
                .Include(x => x.Workouts)
                .Where(x => x.UserId == workout.UserId)
                .SingleAsync(token)
                .ConfigureAwait(false);

            var workouts = user.Workouts!
                .OrderBy(x => x.Position)
                .ToList();

            // The new workout needs to be added to the end of the list.
            workouts.Add(workout);

            for (var i = 0; i < workouts.Count; i++)
            {
                workouts[i].Position = i + 1;
            }

            await dbContext.Workout
                .AddAsync(workout, token)
                .ConfigureAwait(false);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);
            
            await transaction
                .CommitAsync(token)
                .ConfigureAwait(false);

            return workout;
        }
        catch (DbUpdateException ex)
            when (ForeignKeyViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "Workout insert failed, parent user not found.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new ForeignKeyViolationException(nameof(dbContext.Workout), ex);
        }
        catch (DbUpdateException ex)
            when (UniqueConstraintViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "Workout insert failed, workout already exists.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new UniqueConstraintViolationException(nameof(dbContext.Workout), ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error happened inserting workout.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }
}
