namespace ICS.Equipment;

public interface IApiApplicationService
{
    public Task<IEnumerable<EquipmentGroupDto>> GetEquipmentGroups(CancellationToken token);
    public Task<EquipmentGroupDto> GetEquipmentGroup(EquipmentGroupTypes equipmentGroupId, CancellationToken token);
}

public class ApiApplicationService : IApiApplicationService
{
    private readonly ILogger<ApiApplicationService> _logger;

    public ApiApplicationService(
        ILogger<ApiApplicationService> logger)
    {
        _logger = logger;
    }

    public Task<IEnumerable<EquipmentGroupDto>> GetEquipmentGroups(CancellationToken token)
    {
        LogTrace(nameof(GetEquipmentGroups));

        token.ThrowIfCancellationRequested();

        var results = EquipmentGroup.Values;
        
        var dtoResults = results.Select(EquipmentGroupDtoFactory);

        return Task.FromResult(dtoResults);
    }

    public Task<EquipmentGroupDto> GetEquipmentGroup(EquipmentGroupTypes equipmentGroupId, CancellationToken token)
    {
        LogTrace(nameof(GetEquipmentGroup));

        token.ThrowIfCancellationRequested();

        var result = EquipmentGroup.Lookup[equipmentGroupId];

        var dtoResult = EquipmentGroupDtoFactory(result);

        return Task.FromResult(dtoResult);
    }

    private void LogTrace(string methodName) => 
        _logger.LogTrace("Beginning method {ClassName}.{MethodName}", nameof(ApiApplicationService), methodName);

    private static EquipmentGroupDto EquipmentGroupDtoFactory(EquipmentGroup source) =>
        new (source.EquipmentGroupId, source.EquipmentGroupId.Name(), source.EquipmentGroupId.Description());
}
