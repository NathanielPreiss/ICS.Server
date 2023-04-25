using Microsoft.AspNetCore.Mvc.Filters;

namespace ICS.Muscle;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeUserIdAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        return;

        var user = context.HttpContext.Items["User"];
        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}

[ApiController]
[Route("[controller]")]
public class MuscleController : ControllerBase
{
    private readonly IApiApplicationService _apiApplicationService;
    private readonly ILogger<MuscleController> _logger;

    public MuscleController(
        IApiApplicationService apiApplicationService,
        ILogger<MuscleController> logger)
    {
        _apiApplicationService = apiApplicationService;
        _logger = logger;
    }

    [AuthorizeUserId]
    [SwaggerOperation(
        Summary = "Get Body Areas",
        Description = "Get the available body areas.",
        OperationId = nameof(GetBodyAreas)
    )]
    [HttpGet("BodyArea")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A list of body areas.", typeof(IEnumerable<BodyAreaDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetBodyAreas(CancellationToken token)
    {
        try
        {
            Thread.Sleep(5000);
            var results = await _apiApplicationService
                .GetBodyAreas(token).ConfigureAwait(false);

            return Ok(results);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get body areas");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }

    [SwaggerOperation(
        Summary = "Get Body Area",
        Description = "Get a single body area.",
        OperationId = nameof(GetBodyArea)
    )]
    [HttpGet("BodyArea/{bodyAreaId}")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A body area.", typeof(BodyAreaDto))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetBodyArea(
        [FromRoute, SwaggerParameter("Body area identifier.", Required = true)] BodyAreaTypes bodyAreaId,
        CancellationToken token)
    {
        try
        {
            _logger.BeginScope(new {BodyAreaId = bodyAreaId});

            var result = await _apiApplicationService
                .GetBodyArea(bodyAreaId, token).ConfigureAwait(false);

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

    [SwaggerOperation(
        Summary = "Get Muscle Groups",
        Description = "Get the available muscle groups.",
        OperationId = nameof(GetMuscleGroups)
    )]
    [HttpGet("MuscleGroup")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A list of muscle groups.", typeof(IEnumerable<MuscleGroupDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetMuscleGroups(CancellationToken token)
    {
        try
        {
            var results = await _apiApplicationService
                .GetMuscleGroups(token).ConfigureAwait(false);

            return Ok(results);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get muscle groups.");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }

    [SwaggerOperation(
        Summary = "Get Muscle Group",
        Description = "Get a single muscle group.",
        OperationId = nameof(GetMuscleGroup)
    )]
    [HttpGet("MuscleGroup/{muscleGroupId}")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A muscle group.", typeof(MuscleGroupDto))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetMuscleGroup(
        [FromRoute, SwaggerParameter("Muscle group identifier.", Required = true)] MuscleGroupTypes muscleGroupId,
        CancellationToken token)
    {
        try
        {
            _logger.BeginScope(new { MuscleGroupId = muscleGroupId });

            var result = await _apiApplicationService
                .GetMuscleGroup(muscleGroupId, token).ConfigureAwait(false);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get muscle group.");

            return e switch
            {
                // TODO: Id not found
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }

    [SwaggerOperation(
        Summary = "Get Muscles",
        Description = "Get the available muscles.",
        OperationId = nameof(GetMuscles)
    )]
    [HttpGet("Muscle")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A list of muscles.", typeof(IEnumerable<MuscleDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetMuscles(CancellationToken token)
    {
        try
        {
            var results = await _apiApplicationService
                .GetMuscles(token).ConfigureAwait(false);

            return Ok(results);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get muscles.");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }

    [SwaggerOperation(
        Summary = "Get Muscle",
        Description = "Get a single muscle.",
        OperationId = nameof(GetMuscle)
    )]
    [HttpGet("Muscle/{muscleId}")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A muscle.", typeof(MuscleDto))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetMuscle(
        [FromRoute, SwaggerParameter("Muscle identifier.", Required = true)] MuscleTypes muscleId,
        CancellationToken token)
    {
        try
        {
            _logger.BeginScope(new { MuscleId = muscleId });

            var result = await _apiApplicationService
                .GetMuscle(muscleId, token).ConfigureAwait(false);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get muscle.");

            return e switch
            {
                // TODO: Id not found
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }

    [SwaggerOperation(
        Summary = "Get Joints",
        Description = "Get the available joints.",
        OperationId = nameof(GetJoints)
        )]
    [HttpGet("Joints")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A list of joints.", typeof(IEnumerable<JointDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetJoints(CancellationToken token)
    {
        try
        {
            var results = await _apiApplicationService
                .GetJoints(token).ConfigureAwait(false);

            return Ok(results);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get joints.");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }

    [SwaggerOperation(
        Summary = "Get Joint",
        Description = "Get a single joint.",
        OperationId = nameof(GetJoint)
    )]
    [HttpGet("Joint/{jointId}")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A joint.", typeof(JointDto))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> GetJoint(
        [FromRoute, SwaggerParameter("Joint identifier.", Required = true)] JointTypes jointId,
        CancellationToken token)
    {
        try
        {
            _logger.BeginScope(new { JointId = jointId });

            var result = await _apiApplicationService
                .GetJoint(jointId, token).ConfigureAwait(false);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get joint.");

            return e switch
            {
                // TODO: Id not found
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }
}
