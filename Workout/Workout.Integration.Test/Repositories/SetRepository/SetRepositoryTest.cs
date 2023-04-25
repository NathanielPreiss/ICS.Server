namespace ICS.Workout.Test;

[TestClass]
public partial class SetRepositoryTest
{
    private readonly IContainer _container;
    private readonly WorkoutDbContext _dbContext;
    private readonly ISetRepository _unitUnderTest;

    public SetRepositoryTest()
    {
        _container = TestHarness.DefaultContainer();
        _dbContext = _container.Resolve<IDbContextFactory<WorkoutDbContext>>().CreateDbContext();
        _unitUnderTest = _container.Resolve<ISetRepository>();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _container.Dispose();
        _dbContext.Dispose();
    }
}
