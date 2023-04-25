using static ICS.MappingValues;

namespace ICS.Muscle.Test;

[TestClass]
public class MuscleValuesTests
{
    [TestMethod]
    public void BodyArea_Values()
    {
        // Arrange
        var libraryList = BodyArea.Values.Select(x => x.BodyAreaId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void Joint_Values()
    {
        // Arrange
        var libraryList = Joint.Values.Select(x => x.JointId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void JointMuscleGroupMap_Joint_Values()
    {
        // Arrange
        var libraryList = JointMuscleGroupMap.Values.Select(x => x.JointId).Distinct();

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void JointMuscleGroupMap_MuscleGroup_Values()
    {
        // Arrange
        var libraryList = JointMuscleGroupMap.Values.Select(x => x.MuscleGroupId).Distinct();

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void Muscle_Values()
    {
        // Arrange
        var libraryList = Muscle.Values.Select(x => x.MuscleId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void MuscleGroup_Values()
    {
        // Arrange
        var libraryList = MuscleGroup.Values.Select(x => x.MuscleGroupId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }
}
