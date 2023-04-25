namespace ICS.Equipment;

[ApiController]
[Route("[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IApiApplicationService _apiApplicationService;
    private readonly ILogger<EquipmentController> _logger;

    public EquipmentController(
        IApiApplicationService apiApplicationService,
        ILogger<EquipmentController> logger)
    {
        _apiApplicationService = apiApplicationService;
        _logger = logger;
    }

    [SwaggerOperation(
        Summary = "Get Equipment Groups",
        Description = "Get the list of equipment groups.",
        OperationId = nameof(GetEquipmentGroups)
    )]
    [HttpGet("EquipmentGroup")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A list of equipment groups.", typeof(IEnumerable<EquipmentGroupDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetEquipmentGroups(CancellationToken token)
    {
        try
        {
            var results = await _apiApplicationService
                .GetEquipmentGroups(token).ConfigureAwait(false);

            return Ok(results);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get equipment groups");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }

    [SwaggerOperation(
        Summary = "Get Equipment Group",
        Description = "Get a single equipment group.",
        OperationId = nameof(GetEquipmentGroup)
    )]
    [HttpGet("BodyArea/{bodyAreaId}")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A body area.", typeof(EquipmentGroupDto))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetEquipmentGroup(
        [FromRoute, SwaggerParameter("Equipment group identifier.", Required = true)] EquipmentGroupTypes equipmentGroupId,
        CancellationToken token)
    {
        try
        {
            _logger.BeginScope(new { EquipmentGroupId = equipmentGroupId });

            var result = await _apiApplicationService
                .GetEquipmentGroup(equipmentGroupId, token).ConfigureAwait(false);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get body area");

            return e switch
            {
                // TODO: Id not found
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }
}
