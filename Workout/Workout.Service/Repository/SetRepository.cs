namespace ICS.Workout;

/// <summary>
/// The set repository.
/// </summary>
public interface ISetRepository
{
    /// <summary>
    /// Deletes a specific set and updates the remaining set positions.
    /// </summary>
    /// <param name="setId">Set unique id.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The updated list of sets.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<IEnumerable<Set>> DeleteSet(Guid setId, CancellationToken token);

    /// <summary>
    /// Gets a specific set.
    /// </summary>
    /// <param name="setId">Set unique id.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>A single set from the database</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<Set> GetSet(Guid setId, CancellationToken token);

    /// <summary>
    /// Moves a set's id to a new position in the list of sets for a routine.
    /// </summary>
    /// <param name="setId">Set unique id.</param>
    /// <param name="position">The desired position of the set.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>A complete list of the sets since other sets could be moved by the operation.</returns>
    /// <exception cref="EntityNotFoundException" />
    /// <exception cref="UniqueIndexViolationException" />
    public Task<IEnumerable<Set>> MoveSet(Guid setId, int position, CancellationToken token);

    /// <summary>
    /// Patches a set's data, not including key information.
    /// </summary>
    /// <param name="setId">The set id being patched.</param>
    /// <param name="reps">A new rep count.</param>
    /// <param name="weight">A new weight.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The final set object.</returns>
    /// <exception cref="EntityNotFoundException" />
    public Task<Set> PatchSet(Guid setId, int reps, int weight, CancellationToken token);

    /// <summary>
    /// Adds a set at the end of a routine's set list
    /// </summary>
    /// <param name="set">Set that is being added.</param>
    /// <param name="token">Cancellation token support.</param>
    /// <returns>The final set object.</returns>
    /// <exception cref="UniqueConstraintViolationException" />
    /// <exception cref="ForeignKeyViolationException" />
    public Task<Set> PostSet(Set set, CancellationToken token);
}

/// <inheritdoc cref="ISetRepository"/>
public class SetRepository : ISetRepository
{
    private readonly IDbContextFactory<WorkoutDbContext> _dbContextFactory;
    private readonly ILogger<SetRepository> _logger;

    public SetRepository(
        IDbContextFactory<WorkoutDbContext> dbContextFactory,
        ILogger<SetRepository> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task<IEnumerable<Set>> DeleteSet(Guid setId, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var transaction = await dbContext.Database
            .BeginTransactionAsync(token)
            .ConfigureAwait(false);

        try
        {
            var routine = await dbContext.Routine
                .Include(x => x.Sets)
                .Where(x => x.Sets!.Any(y => y.SetId == setId))
                .SingleAsync(token)
                .ConfigureAwait(false);

            var sets = routine.Sets!
                .OrderBy(x => x.Position)
                .ToList();

            var set = sets.Single(x => x.SetId == setId);

            // Remove the records
            sets.Remove(set);
            dbContext.Set.Remove(set);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            // Normalize the position of the remaining sets
            for (var i = 0; i < sets.Count; i++)
            {
                sets[i].Position = i + 1;
            }

            _logger.LogInformation("{Count} remaining sets.", sets.Count);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            await transaction
                .CommitAsync(token)
                .ConfigureAwait(false);

            return sets;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Set was not found");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new EntityNotFoundException(nameof(dbContext.Set), ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error happened deleting set.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }

    public Task<Set> GetSet(Guid setId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Set>> MoveSet(Guid setId, int position, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(token).ConfigureAwait(false);

        var transaction = await dbContext.Database.BeginTransactionAsync(token).ConfigureAwait(false);

        try
        {
            // Get all sets
            var routine = await dbContext.Routine
                .Include(x => x.Sets)
                .Where(x => x.Sets!.Any(y => y.SetId == setId))
                .SingleAsync(token)
                .ConfigureAwait(false);

            var sets = routine.Sets!.ToList();

            var targetSet = sets.Single(x => x.SetId == setId);

            var targetPosition = Math.Min(Math.Max(1, position), sets.Count);

            // Double check we're actually moving
            if (targetSet.Position == targetPosition)
            {
                _logger.LogWarning("Set was not moved.");
                await transaction.RollbackAsync(token).ConfigureAwait(false);
                return sets.OrderBy(x => x.Position);
            }

            var left = Math.Min(targetSet.Position, targetPosition);
            var right = Math.Max(targetSet.Position, targetPosition);

            // Get list of sets that need to move
            var movingSets = sets
                .Where(x => left <= x.Position && x.Position <= right)
                .ToList();

            _logger.LogInformation("Moving {RowCount} sets.", movingSets.Count);

            // This is to free up the current set id
            targetSet.Position = -1;
            await dbContext.SaveChangesAsync(token).ConfigureAwait(false);

            // Move the sets
            var modifier = targetSet.Position > targetPosition ? 1 : -1;
            movingSets.Where(x => x.SetId != setId).ToList().ForEach(x => x.Position += modifier);

            targetSet.Position = targetPosition;

            // Commit the changes
            await dbContext.SaveChangesAsync(token).ConfigureAwait(false);

            await transaction.CommitAsync(token).ConfigureAwait(false);

            return sets.OrderBy(x => x.SetId);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Set was not found");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new EntityNotFoundException(nameof(dbContext.Set), ex);
        }
        catch (DbUpdateException ex)
            when (UniqueIndexViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "An error happened updating set ids");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new UniqueIndexViolationException(nameof(dbContext.Set), ex);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }

    public async Task<Set> PatchSet(Guid setId, int reps, int weight, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        try
        {
            // Get the existing set
            var set = await dbContext.Set
                .SingleAsync(x => x.SetId == setId, token)
                .ConfigureAwait(false);

            // Only update the fields that are allowed
            set.Reps = set.Reps;
            set.Weight = set.Weight;

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            return set;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Set was not found");
            throw new EntityNotFoundException(nameof(dbContext.Set), ex);
        }
    }

    public async Task<Set> PostSet(Set set, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var transaction = await dbContext.Database
            .BeginTransactionAsync(token)
            .ConfigureAwait(false);

        try
        {
            var routine = await dbContext.Routine
                .Include(x => x.Sets)
                .Where(x => x.RoutineId == set.RoutineId)
                .SingleAsync(token)
                .ConfigureAwait(false);

            var sets = routine.Sets!
                .OrderBy(x => x.Position)
                .ToList();

            sets.Add(set);

            for (var i = 0; i < sets.Count; i++)
            {
                sets[i].Position = i + 1;
            }

            await dbContext.Set
                .AddAsync(set, token)
                .ConfigureAwait(false);

            await dbContext
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            await transaction
                .CommitAsync(token)
                .ConfigureAwait(false);

            return set;
        }
        catch (DbUpdateException ex)
            when (ForeignKeyViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "Set insert failed, parent routine not found.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new ForeignKeyViolationException(nameof(dbContext.Set), ex);
        }
        catch (DbUpdateException ex)
            when (UniqueConstraintViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "Set insert failed, set already exists.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw new UniqueConstraintViolationException(nameof(dbContext.Set), ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error happened inserting set.");
            await transaction.RollbackAsync(token).ConfigureAwait(false);
            throw;
        }
    }
}
