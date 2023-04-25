namespace ICS.Workout.Test;

public partial class SetRepositoryTest
{
    [TestMethod]
    [TestCategory(nameof(SetRepository.PostSet))]
    public async Task PostSet()
    {
        // Arrange
        var existingRoutine = await _dbContext.GetRandomRoutine().ConfigureAwait(false);
        var newPosition = existingRoutine.Sets?.Max(x => x.Position) + 1;

        var newSet = Fakers.SetFaker
            .RuleFor(x => x.RoutineId, _ => existingRoutine.RoutineId)
            .RuleFor(x => x.Position, _ => newPosition)
            .Generate();

        // Act
        var result = await _unitUnderTest
            .PostSet(newSet, CancellationToken.None)
            .ConfigureAwait(false);

        // Assert
        var actual = await _dbContext.Set
            .FirstAsync(x =>
                x.RoutineId == newSet.RoutineId &&
                x.SetId == newSet.SetId, CancellationToken.None)
            .ConfigureAwait(false);
    }

    [TestMethod]
    [TestCategory(nameof(SetRepository.PostSet))]
    public async Task PostSet_Exception_DuplicateKey()
    {
        // Arrange
        var existingSet = await _dbContext.GetRandomSet().ConfigureAwait(false);

        var set = Fakers.SetFaker
            .RuleFor(x => x.RoutineId, _ => existingSet.RoutineId)
            .RuleFor(x => x.SetId, _ => existingSet.SetId)
            .Generate();

        // Act
        async Task Action() =>
            await _unitUnderTest
                .PostSet(set, CancellationToken.None)
                .ConfigureAwait(false);

        // Assert
        await Assert.ThrowsExceptionAsync<UniqueConstraintViolationException>(Action);
    }

    [TestMethod]
    [TestCategory(nameof(SetRepository.PostSet))]
    public async Task PostSet_Exception_Cancelled()
    {
        // Arrange
        var set = Fakers.SetFaker.Generate();
        var cancelledToken = new CancellationToken(true);

        // Act
        Task Action() => _unitUnderTest.PostSet(set, cancelledToken);

        // Assert
        await Assert.ThrowsExceptionAsync<TaskCanceledException>(Action).ConfigureAwait(false);
    }
}
