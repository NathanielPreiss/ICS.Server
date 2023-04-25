namespace ICS.Workout;

/// <summary>
/// 
/// </summary>
public interface ISetApplicationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="setId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ICollection<Set>> DeleteSet(Guid setId, CancellationToken token);
    public Task<Set> GetSet(Guid setId, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setId"></param>
    /// <param name="position"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ICollection<Set>> MoveSet(Guid setId, int position, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setId"></param>
    /// <param name="reps"></param>
    /// <param name="weight"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Set> PatchSet(Guid setId, int reps, int weight, CancellationToken token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="set"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Set> PostSet(Set set, CancellationToken token);
}

/// <inheritdoc cref="ISetApplicationService"/>
public class SetApplicationService : ISetApplicationService
{
    private readonly ISetRepository _setRepository;
    private readonly ILogger<SetApplicationService> _logger;

    public SetApplicationService(
        ISetRepository setRepository,
        ILogger<SetApplicationService> logger)
    {
        _setRepository = setRepository;
        _logger = logger;
    }

    public async Task<ICollection<Set>> DeleteSet(Guid setId, CancellationToken token)
    {
        _logger.TraceMethod(nameof(DeleteSet));

        var sets = await _setRepository
            .DeleteSet(setId, token)
            .ConfigureAwait(false);

        return sets.ToList();
    }

    public Task<Set> GetSet(Guid setId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Set>> MoveSet(Guid setId, int position, CancellationToken token)
    {
        _logger.TraceMethod(nameof(MoveSet));

        var sets = await _setRepository
            .MoveSet(setId, position, token)
            .ConfigureAwait(false);

        return sets.ToList();
    }

    public async Task<Set> PatchSet(Guid setId, int reps, int weight, CancellationToken token)
    {
        _logger.TraceMethod(nameof(PatchSet));

        var set = await _setRepository
            .PatchSet(setId, reps, weight, token)
            .ConfigureAwait(false);

        return set;
    }

    public async Task<Set> PostSet(Set set, CancellationToken token)
    {
        set = await _setRepository
            .PostSet(set, token)
            .ConfigureAwait(false);

        return set;
    }
}
