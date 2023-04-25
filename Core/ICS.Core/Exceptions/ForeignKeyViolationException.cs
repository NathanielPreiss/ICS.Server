namespace ICS;

public class ForeignKeyViolationException : IcsException
{
    public static SqlExceptionNumbers Violation => SqlExceptionNumbers.ForeignKeyViolation;

    public static bool IsMatch(DbUpdateException ex) =>
        (ex.InnerException as SqlException)?.Number == (int)Violation;

    private static string MessageBuilder(string tableName) =>
        $"A foreign key constraint was caught for table {tableName}.";

    public readonly string TableName;

    public ForeignKeyViolationException(string tableName, DbUpdateException innerException) :
        base(MessageBuilder(tableName), innerException)
    {
        if (!IsMatch(innerException))
            throw new InvalidCastException($"{nameof(ForeignKeyViolationException)} requires the SqlException violation number to be {Violation}.");

        TableName = tableName;
    }

    public override string ErrorMessage => Exceptions.Resources.ErrorMessage.ForeignKeyViolation;
}
