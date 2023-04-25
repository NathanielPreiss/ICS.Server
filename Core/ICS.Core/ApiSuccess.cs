namespace ICS;

public class ApiSuccess<T> : ApiResult
{
    public T Result { get; set; }

    public ApiSuccess(T result) : 
        base(true)
    {
        Result = result;
    }
}