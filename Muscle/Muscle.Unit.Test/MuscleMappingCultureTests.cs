using static ICS.MappingCulture;

namespace ICS.Muscle.Test;

[TestClass]
public class MuscleMappingCultureTests
{
    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void BodyArea_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<BodyAreaTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name(), value.Description());
        }
    }

    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void Joint_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<JointTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name(), value.Description());
        }
    }

    [Ignore]
    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void Muscle_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<MuscleTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name(), value.Description());
        }
    }

    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void MuscleGroup_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<MuscleGroupTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name(), value.Description());
        }
    }
}
