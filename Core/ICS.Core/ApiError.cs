namespace ICS;

public class ApiError : ApiResult
{
    public string Error { get; set; }

    private ApiError(string errorMessage) :
        base(false)
    {
        Error = errorMessage;
    }

    public ApiError() :
        this(IcsException.UnknownErrorMessage)
    {
    }

    public ApiError(IcsException ex) :
        this(ex.ErrorMessage)
    {
    }
}