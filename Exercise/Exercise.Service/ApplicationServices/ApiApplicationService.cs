namespace ICS.Exercise;

public interface IApiApplicationService
{
    public Task<IEnumerable<ExerciseDto>> GetFilteredExercises(BodyAreaTypes bodyAreaId, int exerciseCount,
        IEnumerable<JointTypes> excludeJoints, IEnumerable<MuscleTypes> excludeMuscles,
        IEnumerable<EquipmentGroupTypes> includeEquipmentGroups, CancellationToken token);
}

public class ApiApplicationService : IApiApplicationService
{
    //private readonly IExerciseDomainService _exerciseDomainService;
    private readonly ILogger<ApiApplicationService> _logger;

    public ApiApplicationService(
        //IExerciseDomainService exerciseDomainService,
        ILogger<ApiApplicationService> logger)
    {
        //_exerciseDomainService = exerciseDomainService;
        _logger = logger;
    }
    public Task<ExerciseDto> GetExercise(ExerciseTypes exerciseId, CancellationToken token)
    {
        _logger.LogTrace("Fetching exercise {ExerciseId}", exerciseId);

        throw new NotImplementedException();

        //var result = await _exerciseDomainService.GetExercise(exerciseId, token).ConfigureAwait(false);

        // Map return results
        //var dtoResult = result.Map();

        //return dtoResult;
    }

    public Task<IEnumerable<ExerciseDto>> GetFilteredExercises(MuscleTypes muscleId, int exerciseCount, CancellationToken token)
    {
        _logger.LogTrace("Fetching {ExerciseCount} exercises for muscle {MuscleId}", exerciseCount, muscleId);

        throw new NotImplementedException();
    }
    public Task<IEnumerable<ExerciseDto>> GetFilteredExercises(BodyAreaTypes bodyAreaId, int exerciseCount,
        IEnumerable<JointTypes> excludeJoints, IEnumerable<MuscleTypes> excludeMuscles,
        IEnumerable<EquipmentGroupTypes> includeEquipmentGroups, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
