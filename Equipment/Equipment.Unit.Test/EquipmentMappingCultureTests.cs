using static ICS.MappingCulture;

namespace ICS.Equipment.Test;

[TestClass]
public class EquipmentMappingCultureTests
{
    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void EquipmentGroup_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<EquipmentGroupTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name(), value.Description());
        }
    }
}
