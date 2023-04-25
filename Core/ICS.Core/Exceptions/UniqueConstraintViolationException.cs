namespace ICS;

public class UniqueConstraintViolationException : IcsException
{
    public static SqlExceptionNumbers Violation => SqlExceptionNumbers.UniqueConstraintViolation;

    public static bool IsMatch(DbUpdateException ex) =>
        (ex.InnerException as SqlException)?.Number == (int)Violation;

    private static string MessageBuilder(string tableName) =>
        $"A unique key constraint was caught for table {tableName}.";

    public readonly string TableName;

    public UniqueConstraintViolationException(string tableName, DbUpdateException innerException) :
        base(MessageBuilder(tableName), innerException)
    {
        if (!IsMatch(innerException))
            throw new InvalidCastException($"{nameof(UniqueConstraintViolationException)} requires the SqlException violation number to be {Violation}.");

        TableName = tableName;
    }

    public override string ErrorMessage => Exceptions.Resources.ErrorMessage.UniqueConstraintViolation;
}
