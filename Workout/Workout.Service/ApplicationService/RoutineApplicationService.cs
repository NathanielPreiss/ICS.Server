using ICS.Workout.Validation;

namespace ICS.Workout;

public interface IRoutineApplicationService
{
    public Task<ICollection<Routine>> DeleteRoutine(Guid routineId, CancellationToken token);
    public Task<Routine> GetRoutine(Guid routineId, CancellationToken token);
    public Task<ICollection<Routine>> MoveRoutine(Guid routineId, int position, CancellationToken token);
    public Task<Routine> PatchRoutine(Guid routineId, ExerciseTypes exerciseId, CancellationToken token);
    public Task<Routine> PostRoutine(Routine routine, CancellationToken token);
}

/// <inheritdoc cref="IRoutineApplicationService"/>
public class RoutineApplicationService : IRoutineApplicationService
{
    private readonly IRoutineRepository _routineRepository;
    private readonly ILogger<RoutineApplicationService> _logger;

    public RoutineApplicationService(
        ISetRepository setRepository,
        IRoutineRepository routineRepository,
        ILogger<RoutineApplicationService> logger)
    {
        _routineRepository = routineRepository;
        _logger = logger;
    }

    public async Task<ICollection<Routine>> DeleteRoutine(Guid routineId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Routine> GetRoutine(Guid routineId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Routine>> MoveRoutine(Guid routineId, int position, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Routine> PatchRoutine(Guid routineId, ExerciseTypes exerciseId, CancellationToken token)
    {
        throw new NotImplementedException();

    }

    public async Task<Routine> PostRoutine(Routine routine, CancellationToken token)
    {
        routine.Validate();

        routine = await _routineRepository
            .PostRoutine(routine, token)
            .ConfigureAwait(false);

        return routine;
    }
}
