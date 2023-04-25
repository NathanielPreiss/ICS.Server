namespace ICS.Library.Test;

[TestClass]
public class ExerciseTest
{
    [TestMethod]
    public void Classification_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<ExerciseTypes>();
        var libraryList = Classification.Values.Select(x => x.ExerciseId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(ExerciseTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void Exercise_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<ExerciseTypes>();
        var libraryList = Exercise.Exercise.Values.Select(x => x.ExerciseId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(ExerciseTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void ExerciseEngagementMuscleMap_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<ExerciseTypes>();
        var libraryList = ExerciseEngagementMuscleMap.Values.Select(x => x.ExerciseId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(ExerciseTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void ExerciseEngagementMuscleMap_Target_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<ExerciseTypes>().Except(new[] { ExerciseTypes.Invalid });
        var libraryList = ExerciseEngagementMuscleMap.Values;

        // Act
        var results = libraryList.Select(x => x.ExerciseEngagements.Target);

        // Assert
        Assert.AreEqual(targetList.Count(), results.Count());
    }

    [TestMethod]
    public void ExerciseEngagementMuscleMap_Synergist_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<ExerciseTypes>().Except(new[] { ExerciseTypes.Invalid });
        var libraryList = ExerciseEngagementMuscleMap.Values;

        // Act
        var results = libraryList.Select(x => x.ExerciseEngagements.Synergists);

        // Assert
        Assert.AreEqual(targetList.Count(), results.Count());
    }

    [TestMethod]
    public void Mechanic_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<MechanicTypes>();
        var libraryList = Mechanic.Values.Select(x => x.MechanicId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(MechanicTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void MuscleEngagement_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<MuscleEngagementTypes>();
        var libraryList = MuscleEngagement.Values.Select(x => x.MuscleEngagementId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(MuscleEngagementTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void Utility_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<UtilityTypes>();
        var libraryList = Utility.Values.Select(x => x.UtilityId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(UtilityTypes.Invalid, missingValues.Single());
    }
}
