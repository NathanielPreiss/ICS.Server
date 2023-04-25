namespace ICS.Workout;

[ApiController]
[Authorize(Policy = Constants.WorkoutIdPolicy)]
[Produces(MediaTypeNames.Application.Json)]
[ResponseCache(Duration = 30)]
[Route("[controller]/{workoutId:guid}")]
[SwaggerResponse(StatusCodes.Status404NotFound, "Description", typeof(ApiError))]
[SwaggerResponse(StatusCodes.Status409Conflict, "Description", typeof(ApiError))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Description", typeof(ApiError))]
public class WorkoutController : ControllerBase
{
    private readonly IRoutineApplicationService _routineApplicationService;
    private readonly IWorkoutApplicationService _workoutApplicationService;
    private readonly ILogger<WorkoutController> _logger;

    public WorkoutController(
        IRoutineApplicationService routineApplicationService,
        IWorkoutApplicationService workoutApplicationService,
        ILogger<WorkoutController> logger)
    {
        _routineApplicationService = routineApplicationService;
        _workoutApplicationService = workoutApplicationService;
        _logger = logger;
    }

    [HttpDelete(Name = nameof(DeleteWorkout))]
    [SwaggerOperation(
        Summary = "Deletes a user's workout.",
        Description = "Deletes a specific workout.",
        OperationId = nameof(DeleteWorkout)
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "A success message.")]
    public async Task<IActionResult> DeleteWorkout(
        [FromRoute, SwaggerParameter("The workout identifier.")] Guid workoutId,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            WorkoutId = workoutId
        });

        try
        {
            await _workoutApplicationService
                .DeleteWorkout(workoutId, token)
                .ConfigureAwait(false);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete user workout.");
            return this.ExceptionResult(ex);
        }
    }

    [HttpGet(Name = nameof(GetWorkout))]
    [SwaggerOperation(
        Summary = "Get a user's workout.",
        Description = "Gets a specific workout.",
        OperationId = nameof(GetWorkout)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(Workout))]
    public async Task<IActionResult> GetWorkout(
        [FromRoute, SwaggerParameter("The workout identifier.")] Guid workoutId,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            WorkoutId = workoutId
        });

        try
        {
            var workout = await _workoutApplicationService
                .GetWorkout(workoutId, token)
                .ConfigureAwait(false);

            return Ok(workout);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get user workout.");
            return this.ExceptionResult(ex);
        }
    }

    [HttpPatch("Move/{position:int}", Name = nameof(PatchMoveWorkout))]
    [SwaggerOperation(
        Summary = "Move a user's workout.",
        Description = "Moves a specific workout to a new position in the list.",
        OperationId = nameof(PatchMoveWorkout)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(IEnumerable<Workout>))]
    public async Task<IActionResult> PatchMoveWorkout(
        [FromRoute, SwaggerParameter("The workout identifier.")] Guid workoutId,
        [FromRoute, SwaggerParameter("The new workout position.")] int position,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            WorkoutId = workoutId,
            Position = position
        });

        try
        {
            var workouts = await _workoutApplicationService
                .MoveWorkout(workoutId, position, token)
                .ConfigureAwait(false);

            return Ok(workouts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to move user workout.");
            return this.ExceptionResult(ex);
        }
    }

    [HttpPatch(Name = nameof(PatchWorkout))]
    [SwaggerOperation(
        Summary = "Patch a user's workout.",
        Description = "Patches a specific workout with new information.",
        OperationId = nameof(PatchWorkout)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(Workout))]
    public async Task<IActionResult> PatchWorkout(
        [FromRoute, SwaggerParameter("The user identifier.")] Guid workoutId,
        [FromBody, SwaggerParameter("The workout information to patch.", Required = true)] PatchWorkoutRequest request,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            WorkoutId = workoutId
        });

        try
        {
            var workout = await _workoutApplicationService
                .PatchWorkout(workoutId, request.Name, token)
                .ConfigureAwait(false);

            return Ok(workout);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to patch user workout.");
            return this.ExceptionResult(ex);
        }
    }

    [Tags("Routine")]
    [HttpPost("Routine", Name = nameof(PostRoutine))]
    [SwaggerOperation(
        Summary = "Post workout routine.",
        Description = "Creates a new routine for a workout.",
        OperationId = nameof(PostRoutine)
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "A success message.", typeof(Routine))]
    public async Task<IActionResult> PostRoutine(
        [FromRoute, SwaggerParameter("The workoutId identifier.")] Guid workoutId,
        [FromBody, SwaggerRequestBody("Newly defined routine.", Required = true)] PostRoutineRequest routineRequest,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            WorkoutId = workoutId
        });

        try
        {
            var routine = this.RoutineFactory(workoutId, routineRequest);

            routine = await _routineApplicationService
                .PostRoutine(routine, token)
                .ConfigureAwait(false);

            return CreatedAtRoute(nameof(RoutineController.GetRoutine), new { routineId = routine.RoutineId }, routine);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed post routine.");
            return this.ExceptionResult(ex);
        }
    }

}
