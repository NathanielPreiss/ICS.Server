using static ICS.MappingCulture;

namespace ICS.Exercise.Test;

[TestClass]
public class ExerciseMappingCultureTests
{
    [Ignore]
    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void Exercise_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<ExerciseTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name(), value.Description());
        }
    }

    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void Mechanic_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<MechanicTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name());
        }
    }

    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void MuscleEngagement_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<MuscleEngagementTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name());
        }
    }

    [DataTestMethod]
    [DynamicData(nameof(GetCultures), typeof(MappingCulture))]
    public void Utility_Mapping(string culture)
    {
        // Arrange
        var valueList = MappingSetup<UtilityTypes>(culture);

        // Act
        foreach (var value in valueList)
        {
            // Assert
            AssertValues(culture, value.Name());
        }
    }
}
