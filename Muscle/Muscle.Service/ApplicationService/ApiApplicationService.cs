namespace ICS.Muscle;

public interface IApiApplicationService
{
    public Task<IEnumerable<BodyAreaDto>> GetBodyAreas(CancellationToken token);
    public Task<BodyAreaDto> GetBodyArea(BodyAreaTypes bodyAreaId, CancellationToken token);
    public Task<IEnumerable<MuscleGroupDto>> GetMuscleGroups(CancellationToken token);
    public Task<MuscleGroupDto> GetMuscleGroup(MuscleGroupTypes muscleGroupId, CancellationToken token);
    public Task<IEnumerable<MuscleDto>> GetMuscles(CancellationToken token);
    public Task<MuscleDto> GetMuscle(MuscleTypes muscleId, CancellationToken token);
    public Task<IEnumerable<JointDto>> GetJoints(CancellationToken token);
    public Task<JointDto> GetJoint(JointTypes jointId, CancellationToken token);
}

public class ApiApplicationService : IApiApplicationService
{
    private readonly ILogger<ApiApplicationService> _logger;

    public ApiApplicationService(
        ILogger<ApiApplicationService> logger)
    {
        _logger = logger;
    }

    public Task<IEnumerable<BodyAreaDto>> GetBodyAreas(CancellationToken token)
    {
        LogTrace(nameof(GetBodyAreas));

        token.ThrowIfCancellationRequested();

        var results = BodyArea.Values;
        
        var dtoResults = results.Select(BodyAreaDtoFactory);

        return Task.FromResult(dtoResults);
    }

    public Task<BodyAreaDto> GetBodyArea(BodyAreaTypes bodyAreaId, CancellationToken token)
    {
        LogTrace(nameof(GetBodyArea));

        token.ThrowIfCancellationRequested();

        var result = BodyArea.Lookup[bodyAreaId];

        var dtoResult = BodyAreaDtoFactory(result);

        return Task.FromResult(dtoResult);
    }

    public Task<IEnumerable<MuscleGroupDto>> GetMuscleGroups(CancellationToken token)
    {
        LogTrace(nameof(GetMuscleGroups));

        token.ThrowIfCancellationRequested();

        var results = MuscleGroup.Values;

        var dtoResults = results.Select(MuscleGroupDtoFactory);

        return Task.FromResult(dtoResults);
    }

    public Task<MuscleGroupDto> GetMuscleGroup(MuscleGroupTypes muscleGroupId, CancellationToken token)
    {
        LogTrace(nameof(GetMuscleGroup));

        token.ThrowIfCancellationRequested();

        var result = MuscleGroup.Lookup[muscleGroupId];

        var dtoResult = MuscleGroupDtoFactory(result);

        return Task.FromResult(dtoResult);
    }

    public Task<IEnumerable<MuscleDto>> GetMuscles(CancellationToken token)
    {
        LogTrace(nameof(GetMuscles));

        token.ThrowIfCancellationRequested();

        var results = Muscle.Values;

        var dtoResults = results.Select(MuscleDtoFactory);

        return Task.FromResult(dtoResults);
    }

    public Task<MuscleDto> GetMuscle(MuscleTypes muscleId, CancellationToken token)
    {
        LogTrace(nameof(GetMuscle));

        token.ThrowIfCancellationRequested();

        var result = Muscle.Lookup[muscleId];

        var dtoResult = MuscleDtoFactory(result);

        return Task.FromResult(dtoResult);
    }

    public Task<IEnumerable<JointDto>> GetJoints(CancellationToken token)
    {
        LogTrace(nameof(GetJoints));

        token.ThrowIfCancellationRequested();

        var results = Joint.Values;

        var dtoResults = results.Select(JointDtoFactory);

        return Task.FromResult(dtoResults);
    }

    public Task<JointDto> GetJoint(JointTypes jointId, CancellationToken token)
    {
        LogTrace(nameof(GetJoint));

        token.ThrowIfCancellationRequested();

        var result = Joint.Lookup[jointId];

        var dtoResult = JointDtoFactory(result);

        return Task.FromResult(dtoResult);
    }

    private void LogTrace(string methodName) => 
        _logger.LogTrace("Beginning method {ClassName}.{MethodName}", nameof(ApiApplicationService), methodName);

    private static BodyAreaDto BodyAreaDtoFactory(BodyArea source) =>
        new (source.BodyAreaId, source.BodyAreaId.Name(), source.BodyAreaId.Description(), source.MuscleGroups.Select(x => x.MuscleGroupId));

    private static MuscleGroupDto MuscleGroupDtoFactory(MuscleGroup source) =>
        new (source.MuscleGroupId, source.MuscleGroupId.Name(), source.MuscleGroupId.Description(), source.BodyAreaId,
            source.Joints.Select(x => x.JointId), source.Muscles.Select(x => x.MuscleId));

    private static MuscleDto MuscleDtoFactory(Muscle source) =>
        new (source.MuscleId, source.MuscleId.Name(), source.MuscleId.Description(), source.MuscleGroupId);

    private static JointDto JointDtoFactory(Joint source) =>
        new(source.JointId, source.JointId.Name(), source.JointId.Description(), source.MuscleGroups.Select(x => x.MuscleGroupId));
}
