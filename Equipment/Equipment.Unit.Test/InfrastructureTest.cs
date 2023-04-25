namespace ICS.Equipment.Test;

[TestClass]
public class InfrastructureTest
{
    [TestMethod]
    public void SchemaCheck()
    {
        // Arrange
        var unitUnderTest = new EquipmentDesignTimeDbContextFactory();

        // Act
        // Assert
        unitUnderTest.CheckForPendingDbContextSchemaChanges();
    }
}
