namespace ICS.Exercise;

[ApiController]
[Route("[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly IApiApplicationService _apiApplicationService;
    private readonly ILogger<ExerciseController> _logger;

    public ExerciseController(
        IApiApplicationService apiApplicationService,
        ILogger<ExerciseController> logger)
    {
        _apiApplicationService = apiApplicationService;
        _logger = logger;
    }

    [SwaggerOperation(
        Summary = "Get Exercises",
        Description = "Gets a list of exercises based on provided filters.",
        OperationId = nameof(GetFilteredExercises)
    )]
    [HttpGet("FilteredExercises")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    //[SwaggerResponse(StatusCodes.Status200OK, "A list of exercises.", typeof(IEnumerable<ExerciseVariantDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetFilteredExercises(
        [FromBody, SwaggerRequestBody("A list of filters to apply to the exercise search.", Required = true)] GetFilteredExercisesRequest request,
        CancellationToken token)
    {
        _logger.LogTrace("Getting a filtered list of exercises");

        try
        {
            var muscleGroups = await _apiApplicationService
                .GetFilteredExercises(request.BodyAreaId, request.ExerciseCount, request.ExcludeJoints, 
                    request.ExcludeMuscles, request.IncludeEquipmentGroups, token).ConfigureAwait(false);

            return Ok(muscleGroups);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get exercises");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }
}
