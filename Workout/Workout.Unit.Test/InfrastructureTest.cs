namespace ICS.Workout.Test;

[TestClass]
public class InfrastructureTest
{
    [TestMethod]
    public void SchemaCheck()
    {
        // Arrange
        var unitUnderTest = new WorkoutDesignTimeDbContextFactory();

        // Act
        // Assert
        unitUnderTest.CheckForPendingDbContextSchemaChanges();
    }
}
