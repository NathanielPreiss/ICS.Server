namespace ICS;

public static class MappingCulture
{
    public static IEnumerable<T> MappingSetup<T>(string culture) where T : Enum
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        return Enum.GetValues(typeof(T)).OfType<T>().Except(new T[] { default! });
    }

    public static List<string[]> GetCultures =>
        Constants.SupportedCultures
            .Select(x => new[] { x })
            .ToList();

    public static void AssertValues(string culture, string name)
    {
        // Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(name), ErrorMessage(culture, nameof(name)));
    }

    public static void AssertValues(string culture, string name, string description)
    {
        // Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(name), ErrorMessage(culture, nameof(name)));
        Assert.IsFalse(string.IsNullOrWhiteSpace(description), ErrorMessage(culture, nameof(description)));
    }

    private static string ErrorMessage(string culture, string fieldName) => $"Field {fieldName} was not given a value for culture {culture}.";
}
