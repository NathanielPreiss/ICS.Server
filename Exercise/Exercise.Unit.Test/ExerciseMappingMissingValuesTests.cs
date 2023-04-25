using static ICS.MappingMissingValues;

namespace ICS.Exercise.Test;

[TestClass]
public class ExerciseMappingMissingValuesTests
{
    [TestMethod]
    public void Exercise_Mapping_MissingValue()
    {
        // Arrange
        const ExerciseTypes value = ExerciseTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<ExerciseTypes>(() => value.Name(), () => value.Description());
    }

    [TestMethod]
    public void Mechanic_Mapping_MissingValue()
    {
        // Arrange
        const MechanicTypes value = MechanicTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<MechanicTypes>(() => value.Name());
    }

    [TestMethod]
    public void MuscleEngagement_Mapping_MissingValue()
    {
        // Arrange
        const MuscleEngagementTypes value = MuscleEngagementTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<MuscleEngagementTypes>(() => value.Name());
    }

    [TestMethod]
    public void Utility_Mapping_MissingValue()
    {
        // Arrange
        const UtilityTypes value = UtilityTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<UtilityTypes>(() => value.Name());
    }
}
