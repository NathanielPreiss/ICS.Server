namespace ICS.Workout.Test;

public partial class RoutineRepositoryTest
{
    [TestMethod]
    [TestCategory(nameof(RoutineRepository.DeleteRoutine))]
    public async Task DeleteRoutine()
    {
        // Arrange
        var existingWorkout = await _dbContext.GetRandomWorkout().ConfigureAwait(false);
        var lastRoutineId = existingWorkout.Routines?.Max(x => x.RoutineId);

        var newRoutine = Fakers.RoutineFaker
            .RuleFor(x => x.WorkoutId, _ => existingWorkout.WorkoutId)
            // TODO .RuleFor(x => x.RoutineId, _ => newRoutineId)
            .Generate();

        // Act
        var result = await _unitUnderTest
            .PostRoutine(newRoutine, CancellationToken.None)
            .ConfigureAwait(false);

        // Assert
        var actual = await _dbContext.Routine
            .FirstAsync(x =>
                x.WorkoutId == newRoutine.WorkoutId &&
                x.RoutineId == newRoutine.RoutineId,
                CancellationToken.None)
            .ConfigureAwait(false);

        // TODO: Assert values
    }

    [DataTestMethod]
    [TestCategory(nameof(RoutineRepository.DeleteRoutine))]
    [DataRow(5)]
    public async Task DeleteRoutine_WithSets(int setCount)
    {
        // Arrange
        var existingWorkout = await _dbContext.GetRandomWorkout().ConfigureAwait(false);
        var newPosition = existingWorkout.Routines?.Max(x => x.Position) + 1;

        var newRoutine = Fakers.RoutineFaker
            .RuleFor(x => x.WorkoutId, _ => existingWorkout.WorkoutId)
            .RuleFor(x => x.Position, _ => newPosition)
            .Generate();

        newRoutine.Sets = Fakers.SetFaker
            .RuleFor(x => x.RoutineId, _ => newRoutine.RoutineId)
            .Generate(setCount);

        // Act
        var result = await _unitUnderTest
            .PostRoutine(newRoutine, CancellationToken.None)
            .ConfigureAwait(false);

        // Assert
        var actual = await _dbContext.Routine
            .Include(x => x.Sets)
            .FirstAsync(x =>
                    x.WorkoutId == newRoutine.WorkoutId &&
                    x.RoutineId == newRoutine.RoutineId,
                CancellationToken.None)
            .ConfigureAwait(false);

        Assert.AreEqual(setCount, actual.Sets?.Count);
        // TODO: Assert values
    }

    [TestMethod]
    [TestCategory(nameof(RoutineRepository.PostRoutine))]
    public async Task DeleteRoutine_Exception_NotFound()
    {
        // Arrange
        var existingWorkout = await _dbContext.GetRandomWorkout().ConfigureAwait(false);

        // Act
        async Task Action() => await _unitUnderTest
            .DeleteRoutine(Guid.NewGuid(), CancellationToken.None)
            .ConfigureAwait(false);

        // Assert
        await Assert.ThrowsExceptionAsync<EntityNotFoundException>(Action);
    }

    [TestMethod]
    [TestCategory(nameof(RoutineRepository.DeleteRoutine))]
    public async Task DeleteRoutine_Exception_Cancelled()
    {
        // Arrange
        var routine = Fakers.RoutineFaker.Generate();
        var cancelledToken = new CancellationToken(true);

        // Act
        async Task Action() => await _unitUnderTest
            .DeleteRoutine(Guid.NewGuid(), cancelledToken)
            .ConfigureAwait(false);

        // Assert
        await Assert.ThrowsExceptionAsync<TaskCanceledException>(Action).ConfigureAwait(false);
    }
}
