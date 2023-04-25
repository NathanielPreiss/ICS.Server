namespace ICS;

public class UniqueIndexViolationException : IcsException
{
    public static SqlExceptionNumbers Violation => SqlExceptionNumbers.UniqueIndexViolation;

    public static bool IsMatch(DbUpdateException ex) =>
        (ex.InnerException as SqlException)?.Number == (int)Violation;

    private static string MessageBuilder(string tableName) =>
        $"A unique index constraint was caught for table {tableName}.";

    public readonly string TableName;

    public UniqueIndexViolationException(string tableName, DbUpdateException innerException) :
        base(MessageBuilder(tableName), innerException)
    {
        if (!IsMatch(innerException))
            throw new InvalidCastException($"{nameof(UniqueIndexViolationException)} requires the SqlException violation number to be {Violation}.");

        TableName = tableName;
    }

    public override string ErrorMessage => Exceptions.Resources.ErrorMessage.UniqueIndexViolation;
}
