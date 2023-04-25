namespace ICS.Auth0;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("[controller]")]
public class UtilityController : ControllerBase
{
    private readonly IUtilityService _utilityService;
    private readonly ILogger<UtilityController> _logger;

    public UtilityController(
        IUtilityService utilityService,
        ILogger<UtilityController> logger)
    {
        _utilityService = utilityService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetTest(CancellationToken token)
    {
        return Ok();
    }


    [SwaggerOperation(
        Summary = "Import All Auth0 Users",
        Description = "Utility for refreshing the environment's Auth0 user list.",
        OperationId = nameof(PostUserRegistrationImport)
    )]
    [HttpPost("Import")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> PostUserRegistrationImport(CancellationToken token)
    {
        try
        {
            await _utilityService
                .ImportAllUsers(token)
                .ConfigureAwait(false);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to import user registrations");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }
}
