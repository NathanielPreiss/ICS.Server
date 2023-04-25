namespace ICS.Workout;

[ApiController]
[Authorize(Policy = Constants.SetIdPolicy)]
[Produces(MediaTypeNames.Application.Json)]
[ResponseCache(Duration = 30)]
[Route("[controller]/{setId:guid}")]
[SwaggerResponse(StatusCodes.Status404NotFound, "Description", typeof(ApiError))]
[SwaggerResponse(StatusCodes.Status409Conflict, "Description", typeof(ApiError))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Description", typeof(ApiError))]
public class SetController : ControllerBase
{
    private readonly ISetApplicationService _setApplicationService;
    private readonly ILogger<SetController> _logger;

    public SetController(
        ISetApplicationService setApplicationService,
        ILogger<SetController> logger)
    {
        _setApplicationService = setApplicationService;
        _logger = logger;
    }

    [HttpDelete(Name = nameof(DeleteSet))]
    [SwaggerOperation(
        Summary = "Delete a set",
        Description = "Removes an existing set from a user's specific routine.",
        OperationId = nameof(DeleteSet)
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "A success message.")]
    public async Task<IActionResult> DeleteSet(
        [FromRoute, SwaggerParameter("The set identifier.")] Guid setId,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            SetId = setId
        });

        try
        {
            await _setApplicationService
                .DeleteSet(setId, token)
                .ConfigureAwait(false);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete set.");
            return this.ExceptionResult(ex);
        }
    }


    [HttpGet(Name = nameof(GetSet))]
    [SwaggerOperation(
        Summary = "Get a set",
        Description = "Gets an existing set.",
        OperationId = nameof(GetSet)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(Set))]
    public async Task<IActionResult> GetSet(
        [FromRoute, SwaggerParameter("The set identifier.")] Guid setId,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            SetId = setId
        });

        try
        {
            var set = await _setApplicationService
                .GetSet(setId, token)
                .ConfigureAwait(false);

            return Ok(set);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get set.");
            return this.ExceptionResult(ex);
        }
    }

    [HttpPatch(Name = nameof(PatchSet))]
    [SwaggerOperation(
        Summary = "Patch a set",
        Description = "Updates an existing set for a user's specific workout.",
        OperationId = nameof(PatchSet)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(Set))]
    public async Task<IActionResult> PatchSet(
        [FromRoute, SwaggerParameter("The set identifier.")] Guid setId,
        [FromBody, SwaggerRequestBody("Request description", Required = true)] PatchSetRequest request,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            SetId = setId
        });

        try
        {
            var set = await _setApplicationService
                .PatchSet(setId, request.Reps, request.Weight, token)
                .ConfigureAwait(false);

            return Ok(set);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete set.");
            return this.ExceptionResult(ex);
        }
    }

    [HttpPatch("Move/{position:int}")]
    [SwaggerOperation(
        Summary = "Move a set",
        Description = "Updates an existing set to a new location.",
        OperationId = nameof(PatchSetMove)
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(ICollection<Set>))]
    public async Task<IActionResult> PatchSetMove(
        [FromRoute, SwaggerParameter("The set identifier.")] Guid setId,
        [FromRoute, SwaggerParameter("The new set identifier.")] int position,
        CancellationToken token)
    {
        _logger.BeginScope(new
        {
            SetId = setId,
            Position = position
        });

        try
        {
            var sets = await _setApplicationService
                .MoveSet(setId, position, token)
                .ConfigureAwait(false);

            return Ok(sets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to move set.");
            return this.ExceptionResult(ex);
        }
    }
}
