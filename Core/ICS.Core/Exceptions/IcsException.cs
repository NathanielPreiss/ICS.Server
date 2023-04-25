namespace ICS;

public abstract class IcsException : Exception
{
    protected IcsException(string message, Exception innerException) :
        base(message, innerException)
    {
    }

    public abstract string ErrorMessage { get; }

    public static string UnknownErrorMessage => Exceptions.Resources.ErrorMessage.UnknownViolation;
}
