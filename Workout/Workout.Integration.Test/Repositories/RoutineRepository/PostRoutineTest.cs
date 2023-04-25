namespace ICS.Workout.Test;

public partial class RoutineRepositoryTest
{
    [TestMethod]
    [TestCategory(nameof(RoutineRepository.PostRoutine))]
    public async Task PostRoutine()
    {
        // Arrange
        var existingWorkout = await _dbContext.GetRandomWorkout().ConfigureAwait(false);
        var newPosition = existingWorkout.Routines?.Max(x => x.Position) ?? + 1;

        var newRoutine = Fakers.RoutineFaker
            .RuleFor(x => x.WorkoutId, _ => existingWorkout.WorkoutId)
            .RuleFor(x => x.Position, _ => newPosition)
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
    [TestCategory(nameof(RoutineRepository.PostRoutine))]
    [DataRow(5)]
    public async Task PostRoutine_WithSets(int setCount)
    {
        // Arrange
        var existingWorkout = await _dbContext.GetRandomWorkout().ConfigureAwait(false);
        var newPosition = existingWorkout.Routines?.Max(x => x.Position) ?? + 1;

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
    public async Task PostRoutine_Exception_DuplicateKey()
    {
        // Arrange
        var existingRoutine = await _dbContext.GetRandomRoutine().ConfigureAwait(false);

        var routine = Fakers.RoutineFaker
            .RuleFor(x => x.WorkoutId, _ => existingRoutine.WorkoutId)
            .RuleFor(x => x.RoutineId, _ => existingRoutine.RoutineId)
            .Generate();

        // Act
        async Task Action() =>
            await _unitUnderTest
                .PostRoutine(routine, CancellationToken.None)
                .ConfigureAwait(false);

        // Assert
        await Assert.ThrowsExceptionAsync<UniqueConstraintViolationException>(Action);
    }

    [TestMethod]
    [TestCategory(nameof(RoutineRepository.PostRoutine))]
    public async Task PostRoutine_Exception_Cancelled()
    {
        // Arrange
        var routine = Fakers.RoutineFaker.Generate();
        var cancelledToken = new CancellationToken(true);

        // Act
        Task Action() => _unitUnderTest.PostRoutine(routine, cancelledToken);

        // Assert
        await Assert.ThrowsExceptionAsync<TaskCanceledException>(Action).ConfigureAwait(false);
    }
}
