namespace ICS.Workout;

[ApiController]
[Authorize(Policy = Constants.WorkoutIdPolicy)]
[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]/{routineId:guid}")]
[SwaggerResponse(StatusCodes.Status404NotFound, "Description", typeof(ApiError))]
[SwaggerResponse(StatusCodes.Status409Conflict, "Description", typeof(ApiError))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Description", typeof(ApiError))]
public class RoutineController : ControllerBase
{
    private readonly IRoutineApplicationService _routineApplicationService;
    private readonly ISetApplicationService _setApplicationService;
    private readonly ILogger<RoutineController> _logger;

    public RoutineController(
        IRoutineApplicationService routineApplicationService,
        ISetApplicationService setApplicationService,
        ILogger<RoutineController> logger)
    {
        _routineApplicationService = routineApplicationService;
        _setApplicationService = setApplicationService;
        _logger = logger;
    }

    [HttpDelete(Name = nameof(DeleteRoutine))]
    [SwaggerOperation(
        Summary = "Delete a routine",
        Description = "Removes an existing routine from a user's specific workout.",
        OperationId = nameof(DeleteRoutine)
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "A success message.")]
    public async Task<IActionResult> DeleteRoutine(
        [FromRoute, SwaggerParameter("The routine identifier.")] Guid routineId,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            RoutineId = routineId
        });

        try
        {
            await _routineApplicationService
                .DeleteRoutine(routineId, token)
                .ConfigureAwait(false);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete routine.");
            return this.ExceptionResult(ex);
        }
    }

    [HttpGet(Name = nameof(GetRoutine))]
    [SwaggerOperation(
        Summary = "Get a routine",
        Description = "Gets an routine.",
        OperationId = nameof(GetRoutine)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(Routine))]
    public async Task<IActionResult> GetRoutine(
        [FromRoute, SwaggerParameter("The routine identifier.")] Guid routineId,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            RoutineId = routineId
        });

        try
        {
            var routine = await _routineApplicationService
                .GetRoutine(routineId, token)
                .ConfigureAwait(false);

            return Ok(routine);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get routine.");
            return this.ExceptionResult(ex);
        }
    }

    [HttpPatch(Name = nameof(PatchRoutine))]
    [SwaggerOperation(
        Summary = "Patch a routine",
        Description = "Update an existing routine for a user.",
        OperationId = nameof(PatchRoutine)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(Routine))]
    public async Task<IActionResult> PatchRoutine(
        [FromRoute, SwaggerParameter("The routine identifier.")] Guid routineId,
        [FromBody, SwaggerRequestBody("Request description", Required = true)] PatchRoutineRequest request,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            RoutineId = routineId
        });

        try
        {
            var routine = await _routineApplicationService
                .PatchRoutine(routineId, request.ExerciseId, token)
                .ConfigureAwait(false);

            return Ok(routine);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to patch routine.");
            return this.ExceptionResult(ex);
        }
    }

    [HttpPatch("Move/{position:int}", Name = nameof(PatchMoveRoutine))]
    [SwaggerOperation(
        Summary = "Move a routine",
        Description = "Updates an existing routine to a new location.",
        OperationId = nameof(PatchMoveRoutine)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(IEnumerable<Routine>))]
    public async Task<IActionResult> PatchMoveRoutine(
        [FromRoute, SwaggerParameter("The routine identifier.")] Guid routineId,
        [FromRoute, SwaggerParameter("The new routine position.")] int position,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            RoutineId = routineId,
            Position = position
        });

        try
        {
            var routines = await _routineApplicationService
                .MoveRoutine(routineId, position, token)
                .ConfigureAwait(false);

            return Ok(routines);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to move routine.");
            return this.ExceptionResult(ex);
        }
    }

    [Tags("Set")]
    [HttpPost("Set", Name = nameof(PostSet))]
    [SwaggerOperation(
        Summary = "Post routine set.",
        Description = "Creates a new set for a routine.",
        OperationId = nameof(PostSet)
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "A success message.", typeof(Set))]
    public async Task<IActionResult> PostSet(
        [FromRoute, SwaggerParameter("The routine identifier.")] Guid routineId,
        [FromBody, SwaggerRequestBody("Request description", Required = true)] PostSetRequest setRequest,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            RoutineId = routineId
        });

        try
        {
            var set = this.SetFactory(routineId, setRequest);

            set = await _setApplicationService
                .PostSet(set, token)
                .ConfigureAwait(false);

            return CreatedAtRoute(nameof(SetController.GetSet), new { setId = set.SetId}, set);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to post set.");
            return this.ExceptionResult(ex);
        }
    }
}
