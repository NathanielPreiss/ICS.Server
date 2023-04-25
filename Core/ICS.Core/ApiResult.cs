namespace ICS;

public abstract class ApiResult
{
    public bool Success { get; }

    protected ApiResult(bool isSuccess)
    {
        Success = isSuccess;
    }
}