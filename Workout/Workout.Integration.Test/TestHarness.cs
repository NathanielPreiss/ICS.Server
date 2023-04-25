namespace ICS.Workout.Test;

[TestClass]
public static class TestHarness
{
    private const string ConfigJson = "appsettings.json";
    private const string WorkoutConnectionStringName = "WorkoutDatabase";
    private const string ApplicationName = "Integration Tests";
    private const int SeedUserCount = 100;
    private const int SeedWorkoutUpperLimit = 5;
    private const int SeedRoutineUpperLimit = 5;
    private const int SeedSetUpperLimit = 3;

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext _)
    {
        CommonTestHarness.AssemblyInitialize(_);
        
        Console.WriteLine(@"AssemblyInitialize");

        var config = CommonTestHarness.GetConfiguration(ConfigJson);

        var contextOptions = new DbContextOptionsBuilder<WorkoutDbContext>()
            .UseSqlServer(config.GetConnectionString(WorkoutConnectionStringName))
            .Options;

#if !DEBUG

        using var deleteDbContext = new WorkoutDbSeedContext(contextOptions);

        deleteDbContext.Database.EnsureDeleted();

#endif

        using var createDbContext = new WorkoutDbSeedContext(SeedUserCount, SeedWorkoutUpperLimit, SeedRoutineUpperLimit, SeedSetUpperLimit, contextOptions);

        createDbContext.Database.EnsureCreated();

        Console.WriteLine(@"Database Created");
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        Console.WriteLine(@"AssemblyCleanup");

#if !DEBUG

        var config = CommonTestHarness.GetConfiguration(ConfigJson);

        var contextOptions = new DbContextOptionsBuilder<WorkoutDbContext>()
            .UseSqlServer(config.GetConnectionString(WorkoutConnectionStringName))
            .Options;

        using var deleteDbContext = new WorkoutDbSeedContext(contextOptions);

        deleteDbContext.Database.EnsureDeleted();

        Console.WriteLine(@"Database Deleted");

#endif

    }

    public static IContainer DefaultContainer() =>
        CommonTestHarness.DefaultContainer(ApplicationName, ConfigJson, WorkoutConnectionStringName);
}
