namespace ICS.Workout.Test;

[TestClass]
public partial class RoutineRepositoryTest
{
    private readonly IContainer _container;
    private readonly WorkoutDbContext _dbContext;
    private readonly IRoutineRepository _unitUnderTest;

    public RoutineRepositoryTest()
    {
        _container = TestHarness.DefaultContainer();
        _dbContext = _container.Resolve<IDbContextFactory<WorkoutDbContext>>().CreateDbContext();
        _unitUnderTest = _container.Resolve<IRoutineRepository>();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _container.Dispose();
        _dbContext.Dispose();
    }
}
