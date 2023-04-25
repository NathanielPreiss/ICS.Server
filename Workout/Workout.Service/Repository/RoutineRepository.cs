namespace ICS.Workout;

/// <summary>
/// Repository for updating workout routines
/// </summary>
public interface IRoutineRepository
{
    /// <summary>
    /// Removes a routine and its attached sets from the database.
    /// </summary>
    /// <param name="routineId">The routine's unique id.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <exception cref="EntityNotFoundException" />
    public Task<ICollection<Routine>> DeleteRoutine(Guid routineId, CancellationToken token);

    /// <summary>
    /// Gets a routine from the database.
    /// </summary>
    /// <param name="routineId">The routine id to search for.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>A routine and it's included sets.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<Routine> GetRoutine(Guid routineId, CancellationToken token);

    /// <summary>
    /// Moves a routine to a new position in the workout.
    /// </summary>
    /// <param name="routineId">The routine being moved.</param>
    /// <param name="position">The new position of the routine.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The updated list of routines.</returns>
    /// <exception cref="EntityNotFoundException" />
    /// <exception cref="UniqueIndexViolationException" />
    public Task<ICollection<Routine>> MoveRoutine(Guid routineId, int position, CancellationToken token);

    /// <summary>
    /// Updates a routine's information.
    /// </summary>
    /// <param name="routineId">The routine being updated.</param>
    /// <param name="exerciseId">A new exercise for the routine.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The final state of the updated routine.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<Routine> PatchRoutine(Guid routineId, ExerciseTypes exerciseId, CancellationToken token);

    /// <summary>
    /// Adds a new routine to the database.
    /// </summary>
    /// <param name="routine">The routine being added to the database.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The final state of the new routine.</returns>
    /// <exception cref="UniqueConstraintViolationException" />
    /// <exception cref="ForeignKeyViolationException" />
    public Task<Routine> PostRoutine(Routine routine, CancellationToken token);
}

/// <inheritdoc cref="IRoutineRepository"/>>
public class RoutineRepository : IRoutineRepository
{
    private readonly IDbContextFactory<WorkoutDbContext> _dbContextFactory;
    private readonly ILogger<RoutineRepository> _logger;

    public RoutineRepository(
        IDbContextFactory<WorkoutDbContext> dbContextFactory,
        ILogger<RoutineRepository> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task<ICollection<Routine>> DeleteRoutine(Guid routineId, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var transaction = await dbContext.Database
            .BeginTransactionAsync(token)
            .ConfigureAwait(false);

        try
        {
            var workout = await dbContext.Workout
                .Include(x => x.Routines)
                .Where(x => x.Routines!.Any(y => y.RoutineId == routineId))
                .SingleAsync(token)
                .ConfigureAwait(false);

            var routines = workout.Routines!
                .OrderBy(x => x.Position)
                .ToList();

            var routine = routines.Single(x => x.RoutineId == routineId);

            // Remove the records
            dbContext.Set.RemoveRange(routine.Sets ?? new List<Set>());
            dbContext.Routine.Remove(routine);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            routines.Remove(routine);

            // Normalize the position of the remaining routines
            for (var i = 0; i < routines.Count; i++)
            {
                routines[i].Position = i + 1;
            }

            _logger.LogInformation("{Count} remaining routines.", routines.Count);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            await transaction
                .CommitAsync(token)
                .ConfigureAwait(false);

            return routines;

        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Routine was not found");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new EntityNotFoundException(nameof(dbContext.Routine), ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error happened removing the routine.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }
    
    public async Task<Routine> GetRoutine(Guid routineId, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        try
        {
            var routine = await dbContext.Routine
                .Include(x => x.Sets)
                .SingleAsync(x => x.RoutineId == routineId, token)
                .ConfigureAwait(false);

            return routine;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Routine was not found");
            throw new EntityNotFoundException(nameof(dbContext.Routine), ex);
        }
    }

    public async Task<ICollection<Routine>> MoveRoutine(Guid routineId, int position, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var transaction = await dbContext.Database
            .BeginTransactionAsync(token)
            .ConfigureAwait(false);

        try
        {
            var workout = await dbContext.Workout
                .Include(x => x.Routines)
                .Where(x => x.Routines!.Any(y => y.RoutineId == routineId))
                .SingleAsync(token)
                .ConfigureAwait(false);

            var routines = workout.Routines!
                .OrderBy(x => x.Position)
                .ToList();

            var routine = routines.Single(x => x.RoutineId == routineId);

            var finalPosition = Math.Min(Math.Max(1, position), routines.Count);

            // This is to free up the current set id
            routine.Position = -1;

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            // Move the routine to the correct position in the list
            routines.Remove(routine);
            routines.Insert(finalPosition - 1, routine);

            // Normalize the position of the routines
            for (var i = 0; i < routines.Count; i++)
            {
                routines[i].Position = i + 1;
            }

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            await transaction
                .CommitAsync(token)
                .ConfigureAwait(false);

            return routines;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Routine was not found");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new EntityNotFoundException(nameof(dbContext.Routine), ex);
        }
        catch (DbUpdateException ex)
            when (UniqueIndexViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "An error happened updating routine position.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new UniqueIndexViolationException(nameof(dbContext.Routine), ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error happened updating routine position.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }

    public async Task<Routine> PatchRoutine(Guid routineId, ExerciseTypes exerciseId, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        try
        {
            var routine = await dbContext.Routine
                .SingleAsync(x => x.RoutineId == routineId, token)
                .ConfigureAwait(false);

            // Update the valid fields
            routine.ExerciseId = exerciseId;

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            return routine;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Routine was not found");
            throw new EntityNotFoundException(nameof(dbContext.Routine), ex);
        }
    }

    public async Task<Routine> PostRoutine(Routine routine, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var transaction = await dbContext.Database
            .BeginTransactionAsync(token)
            .ConfigureAwait(false);

        try
        {
            var workout = await dbContext.Workout
                .Include(x => x.Routines)
                .Where(x => x.WorkoutId == routine.WorkoutId)
                .SingleAsync(token)
                .ConfigureAwait(false);

            var routines = workout.Routines!
                .OrderBy(x => x.Position)
                .ToList();

            routines.Add(routine);

            for (var i = 0; i < routines.Count; i++)
            {
                routines[i].Position = i + 1;
            }

            await dbContext.Routine
                .AddAsync(routine, token)
                .ConfigureAwait(false);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            await transaction
                .CommitAsync(token)
                .ConfigureAwait(false);

            return routine;
        }
        catch (DbUpdateException ex)
            when (ForeignKeyViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "Routine insert failed, parent workout not found.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new ForeignKeyViolationException(nameof(dbContext.Routine), ex);
        }
        catch (DbUpdateException ex)
            when (UniqueConstraintViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "Routine insert failed, routine already exists.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new UniqueConstraintViolationException(nameof(dbContext.Routine), ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error happened inserting routine.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }
}
