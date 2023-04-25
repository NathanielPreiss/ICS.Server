namespace ICS;

// TODO: Inherit IcsException
public class MissingResourceMappingException : ArgumentOutOfRangeException
{
    public MissingResourceMappingException(string mapFieldName, string fieldName, Enum fieldValue) 
        : base(fieldName, fieldValue, MessageBuilder(mapFieldName, fieldValue))
    {
    }

    private static string MessageBuilder(string mapFieldName, Enum fieldValue) =>
        $"Unable to find a {mapFieldName} resource for value {fieldValue} of type {fieldValue.GetType()}.";
}
