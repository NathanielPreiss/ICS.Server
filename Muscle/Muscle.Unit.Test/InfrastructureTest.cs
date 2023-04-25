namespace ICS.Muscle.Test;

[TestClass]
public class InfrastructureTest
{
    [TestMethod]
    public void SchemaCheck()
    {
        // Arrange
        var unitUnderTest = new MuscleDesignTimeDbContextFactory();

        // Act
        // Assert
        unitUnderTest.CheckForPendingDbContextSchemaChanges();
    }
}
