namespace ICS;

public static class MappingValues
{
    public static void AssertLibraryValues<T>(IEnumerable<T> libraryValues) where T : Enum
    {
        // Arrange
        var expectedValues = (T[])Enum.GetValues(typeof(T));

        // Act
        var missingValues = expectedValues.Except(libraryValues).ToList();

        // Assert
        Assert.IsTrue(missingValues.Remove(default!), "Default \"Invalid\" value should not be populated in values.");
        Assert.AreEqual(0, missingValues.Count, $"Values are missing {string.Join(", ", missingValues)}");
    }
}
