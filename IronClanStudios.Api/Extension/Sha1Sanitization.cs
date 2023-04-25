namespace ICS.Api;

public static class Sha1Sanitization
{
    private const int EntityPathOrNameMaxLength = 50;

    public static string SanitizeType(Type type) => SanitizeName(type.FullName ?? throw new InvalidOperationException());

    public static string SanitizeName(string entityPathOrName)
    {
        var rgx = new Regex(@"[^a-zA-Z0-9\-\._]");
        entityPathOrName = rgx.Replace(entityPathOrName, string.Empty);

        if (entityPathOrName.Length > EntityPathOrNameMaxLength)
            entityPathOrName = Build(entityPathOrName);

        return entityPathOrName;
    }

    private static string Build(string input)
    {
        using var provider = SHA1.Create();
        var inputBytes = Encoding.Default.GetBytes(input);
        var hashBytes = provider.ComputeHash(inputBytes);

        var hashBuilder = new StringBuilder(string.Join(string.Empty, hashBytes.Select(x => x.ToString("x2"))));
        foreach (var delimiterIndex in new[] { 5, 11, 17, 23, 29, 35, 41 })
            hashBuilder.Insert(delimiterIndex, "-");
        return hashBuilder.ToString();
    }
}