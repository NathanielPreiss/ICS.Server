using static ICS.MappingValues;

namespace ICS.Equipment.Test;

[TestClass]
public class EquipmentValuesTests
{
    [TestMethod]
    public void EquipmentGroup_Values()
    {
        // Arrange
        var libraryList = EquipmentGroup.Values.Select(x => x.EquipmentGroupId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }
}
