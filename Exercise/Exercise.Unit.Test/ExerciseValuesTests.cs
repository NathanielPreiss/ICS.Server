using static ICS.MappingValues;

namespace ICS.Exercise.Test;

[TestClass]
public class ExerciseValuesTests
{
    [TestMethod]
    public void Exercise_Values()
    {
        // Arrange
        var libraryValues = Exercise.Values.Select(x => x.ExerciseId);

        // Act
        // Assert
        AssertLibraryValues(libraryValues);
    }

    [TestMethod]
    public void Classification_Values()
    {
        // Arrange
        var libraryList = Classification.Values.Select(x => x.ExerciseId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void ExerciseEngagementMuscleMap_Values()
    {
        // Arrange
        var libraryList = ExerciseEngagementMuscleMap.Values.Select(x => x.ExerciseId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void Mechanic_Values()
    {
        // Arrange
        var libraryList = Mechanic.Values.Select(x => x.MechanicId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void MuscleEngagement_Values()
    {
        // Arrange
        var libraryList = MuscleEngagement.Values.Select(x => x.MuscleEngagementId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }

    [TestMethod]
    public void Utility_Values()
    {
        // Arrange
        var libraryList = Utility.Values.Select(x => x.UtilityId);

        // Act
        // Assert
        AssertLibraryValues(libraryList);
    }
}
