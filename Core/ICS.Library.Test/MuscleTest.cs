namespace ICS.Library.Test;

[TestClass]
public class MuscleTest
{
    [TestMethod]
    public void BodyArea_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<BodyAreaTypes>();
        var libraryList = BodyArea.Values.Select(x => x.BodyAreaId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(BodyAreaTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void Joint_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<JointTypes>();
        var libraryList = Joint.Values.Select(x => x.JointId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(JointTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void JointMuscleGroupMap_Joint_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<JointTypes>();
        var libraryList = JointMuscleGroupMap.Values.Select(x => x.JointId).Distinct();

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(JointTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void JointMuscleGroupMap_MuscleGroup_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<MuscleGroupTypes>();
        var libraryList = JointMuscleGroupMap.Values.Select(x => x.MuscleGroupId).Distinct();

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(MuscleGroupTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void Muscle_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<MuscleTypes>();
        var libraryList = Muscle.Muscle.Values.Select(x => x.MuscleId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(MuscleTypes.Invalid, missingValues.Single());
    }

    [TestMethod]
    public void MuscleGroup_Values()
    {
        // Arrange
        var targetList = Enum.GetValues<MuscleGroupTypes>();
        var libraryList = MuscleGroup.Values.Select(x => x.MuscleGroupId);

        // Act
        var missingValues = targetList.Except(libraryList);

        // Assert
        Assert.AreEqual(MuscleGroupTypes.Invalid, missingValues.Single());
    }
}
