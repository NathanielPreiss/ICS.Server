namespace ICS.Auth0.Test;

[TestClass]
public class InfrastructureTest
{
    [TestMethod]
    public void SchemaCheck()
    {
        // Arrange
        var unitUnderTest = new Auth0DesignTimeDbContextFactory();

        // Act
        // Assert
        unitUnderTest.CheckForPendingDbContextSchemaChanges();
    }
}
