namespace ICS;

public class EntityNotFoundException : IcsException
{
    public static SqlExceptionNumbers Violation => SqlExceptionNumbers.UniqueConstraintViolation;

    private static string MessageBuilder(string tableName) =>
        $"A resource entity could not be found in table {tableName}.";

    public readonly string TableName;

    public EntityNotFoundException(string tableName, InvalidOperationException innerException) :
        base(MessageBuilder(tableName), innerException)
    {
        TableName = tableName;
    }

    public override string ErrorMessage => Exceptions.Resources.ErrorMessage.EntityNotFoundViolation;
}
