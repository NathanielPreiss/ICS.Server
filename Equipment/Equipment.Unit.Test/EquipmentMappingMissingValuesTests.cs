using static ICS.MappingMissingValues;

namespace ICS.Equipment.Test;

[TestClass]
public class EquipmentMappingMissingValuesTests
{
    [TestMethod]
    public void EquipmentGroup_Mapping_MissingValue()
    {
        // Arrange
        const EquipmentGroupTypes value = EquipmentGroupTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<EquipmentGroupTypes>(() => value.Name(), () => value.Description());
    }
}
