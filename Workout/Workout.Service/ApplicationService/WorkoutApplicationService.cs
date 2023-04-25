namespace ICS.Workout;

/// <summary>
/// 
/// </summary>
public interface IWorkoutApplicationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ICollection<Workout>> DeleteWorkout(Guid workoutId, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Workout> GetWorkout(Guid workoutId, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ICollection<Workout>> GetWorkouts(Guid userId, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="position"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ICollection<Workout>> MoveWorkout(Guid workoutId, int position, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="name"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Workout> PatchWorkout(Guid workoutId, string name, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="workout"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Workout> PostWorkout(Workout workout, CancellationToken token);
}

/// <inheritdoc cref="IWorkoutApplicationService"/>
public class WorkoutApplicationService : IWorkoutApplicationService
{
    private readonly IUserRepository _userRepository;
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ILogger<WorkoutApplicationService> _logger;

    public WorkoutApplicationService(
        IUserRepository userRepository,
        IWorkoutRepository workoutRepository,
        ILogger<WorkoutApplicationService> logger)
    {
        _userRepository = userRepository;
        _workoutRepository = workoutRepository;
        _logger = logger;
    }

    public async Task<ICollection<Workout>> DeleteWorkout(Guid workoutId, CancellationToken token)
    {
        var workouts = await _workoutRepository
            .DeleteWorkout(workoutId, token)
            .ConfigureAwait(false);

        return workouts;
    }

    public async Task<Workout> GetWorkout(Guid workoutId, CancellationToken token)
    {
        var workout = await _workoutRepository
            .GetWorkout(workoutId, token)
            .ConfigureAwait(false);

        return workout;
    }

    public async Task<ICollection<Workout>> GetWorkouts(Guid userId, CancellationToken token)
    {
        var workouts = await _userRepository
            .GetWorkouts(userId, token)
            .ConfigureAwait(false);

        return workouts;
    }

    public async Task<ICollection<Workout>> MoveWorkout(Guid workoutId, int position, CancellationToken token)
    {
        var workouts = await _workoutRepository
            .MoveWorkout(workoutId, position, token)
            .ConfigureAwait(false);

        return workouts;
    }

    public async Task<Workout> PatchWorkout(Guid workoutId, string name, CancellationToken token)
    {
        var workout = await _workoutRepository
            .PatchWorkout(workoutId, name, token)
            .ConfigureAwait(false);

        return workout;
    }

    public async Task<Workout> PostWorkout(Workout workout, CancellationToken token)
    {
        workout.Validate();

        workout = await _workoutRepository
            .PostWorkout(workout, token)
            .ConfigureAwait(false);

        return workout;
    }
}
