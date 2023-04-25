namespace ICS;

public static class LoggingExtension
{
    public static void TraceMethod(this ILogger log, string methodName)
    {
        log.LogTrace("Beginning method {MethodName}", methodName);
    }
}
