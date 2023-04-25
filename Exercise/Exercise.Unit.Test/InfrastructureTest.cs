namespace ICS.Exercise.Test;

[TestClass]
public class InfrastructureTest
{
    [TestMethod]
    public void SchemaCheck()
    {
        // Arrange
        var unitUnderTest = new ExerciseDesignTimeDbContextFactory();

        // Act
        // Assert
        unitUnderTest.CheckForPendingDbContextSchemaChanges();
    }
}
