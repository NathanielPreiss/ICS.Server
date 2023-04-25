namespace ICS.Auth0;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class Auth0Controller : ControllerBase
{
    private readonly IWebHookApplicationService _webHookApplicationService;
    private readonly ILogger<Auth0Controller> _logger;

    public Auth0Controller(
        IWebHookApplicationService webHookApplicationService,
        ILogger<Auth0Controller> logger)
    {
        _webHookApplicationService = webHookApplicationService;
        _logger = logger;
    }

    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = nameof(PostPreRegistration)
    )]
    [HttpPost("PreRegistration")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.", typeof(PreRegistrationResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> PostPreRegistration(
        [FromBody] PreRegistrationRequest request,
        CancellationToken token)
    {
        try
        {
            var userId = await _webHookApplicationService
                .PreRegistration(Guid.NewGuid(), request.Email, token)
                .ConfigureAwait(false);

            var result = new PreRegistrationResponse(userId);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to process pre registration");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }

    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = nameof(PostRegistration)
    )]
    [HttpPost("PostRegistration")]
    [Produces("application/json")]
    [ResponseCache(Duration = 30)]
    [SwaggerResponse(StatusCodes.Status200OK, "A success message.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown internal error.", typeof(string))]
    public async Task<IActionResult> PostRegistration(
        [FromBody] PostRegistrationRequest request,
        CancellationToken token)
    {
        try
        {
            await _webHookApplicationService
                .PostRegistration(request.IcsUserId, request.ProviderIdentity, token)
                .ConfigureAwait(false);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to process pre registration");

            return e switch
            {
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown internal error.")
            };
        }
    }
}
