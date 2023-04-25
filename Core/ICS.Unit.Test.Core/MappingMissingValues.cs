namespace ICS;

public static class MappingMissingValues
{
    public static void AssertExceptions<T>(Func<string> name)
    {
        // Assert
        Assert.ThrowsException<MissingResourceMappingException>(name, ErrorMessage<T>(nameof(name)));
    }

    public static void AssertExceptions<T>(Func<string> name, Func<string> description)
    {
        // Assert
        Assert.ThrowsException<MissingResourceMappingException>(name, ErrorMessage<T>(nameof(name)));
        Assert.ThrowsException<MissingResourceMappingException>(description, ErrorMessage<T>(nameof(description)));
    }

    private static string ErrorMessage<T>(string field) => $"Field {field} did not throw an exception for the Invalid value for {typeof(T)}.";
}
