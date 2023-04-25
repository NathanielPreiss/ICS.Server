using static ICS.MappingMissingValues;

namespace ICS.Muscle.Test;

[TestClass]
public class MuscleMappingMissingValuesTests
{
    [TestMethod]
    public void BodyArea_Mapping_MissingValue()
    {
        // Arrange
        const BodyAreaTypes value = BodyAreaTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<BodyAreaTypes>(() => value.Name(), () => value.Description());
    }

    [TestMethod]
    public void Joint_Mapping_MissingValue()
    {
        // Arrange
        const JointTypes value = JointTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<JointTypes>(() => value.Name(), () => value.Description());
    }

    [TestMethod]
    public void Muscle_Mapping_MissingValue()
    {
        // Arrange
        const MuscleTypes value = MuscleTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<MuscleTypes>(() => value.Name(), () => value.Description());
    }

    [TestMethod]
    public void MuscleGroup_Mapping_MissingValue()
    {
        // Arrange
        const MuscleGroupTypes value = MuscleGroupTypes.Invalid;

        // Act
        // Assert
        AssertExceptions<MuscleGroupTypes>(() => value.Name(), () => value.Description());
    }
}
