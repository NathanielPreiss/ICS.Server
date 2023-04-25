namespace ICS.Workout;

[ApiController]
[Authorize(Policy = Constants.UserIdPolicy)]
[Produces(MediaTypeNames.Application.Json)]
[ResponseCache(Duration = 30)]
[Route("[controller]/{userId:guid}")]
[SwaggerResponse(StatusCodes.Status404NotFound, "Description", typeof(ApiError))]
[SwaggerResponse(StatusCodes.Status409Conflict, "Description", typeof(ApiError))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Description", typeof(ApiError))]
public class UserController : ControllerBase
{
    private readonly IWorkoutApplicationService _workoutApplicationService;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IWorkoutApplicationService workoutApplicationService,
        ILogger<UserController> logger)
    {
        _workoutApplicationService = workoutApplicationService;
        _logger = logger;
    }

    [Tags("Workout")]
    [HttpGet("Workout", Name = nameof(GetWorkouts))]
    [SwaggerOperation(
        Summary = "Get user's workouts.",
        Description = "Gets all of a users existing workouts.",
        OperationId = nameof(GetWorkouts)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(IEnumerable<Workout>))]
    public async Task<IActionResult> GetWorkouts(
        [FromRoute, SwaggerParameter("The user identifier.")] Guid userId,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            UserId = userId
        });

        try
        {
            var workouts = await _workoutApplicationService
                .GetWorkouts(userId, token)
                .ConfigureAwait(false);

            return Ok(workouts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed get user workouts.");
            return this.ExceptionResult(ex);
        }
    }

    [Tags("Workout")]
    [HttpPost("Workout", Name = nameof(PostWorkout))]
    [SwaggerOperation(
        Summary = "Post user workout.",
        Description = "Creates a new workout for a user.",
        OperationId = nameof(PostWorkout)
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "A success message.", typeof(Workout))]
    public async Task<IActionResult> PostWorkout(
        [FromRoute, SwaggerParameter("The user identifier.")] Guid userId,
        [FromBody, SwaggerRequestBody("Newly defined workout.", Required = true)] PostWorkoutRequest workoutRequest,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            UserId = userId
        });

        try
        {
            var workout = this.WorkoutFactory(userId, workoutRequest);
            
            workout = await _workoutApplicationService
                .PostWorkout(workout, token)
                .ConfigureAwait(false);

            return CreatedAtRoute(nameof(WorkoutController.GetWorkout), new { workoutId = workout.WorkoutId}, workout);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed post workout.");
            return this.ExceptionResult(ex);
        }
    }
}
