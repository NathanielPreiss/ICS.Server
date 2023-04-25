namespace ICS.Workout;

public class WorkoutModule : Module
{
    private readonly string _connectionString;
    private readonly string _applicationName;
    private readonly bool _isProduction;

    public WorkoutModule(string applicationName, bool isProduction, string connectionString)
    {
        _applicationName = applicationName;
        _isProduction = isProduction;
        _connectionString = connectionString;
    }

    /// <summary>
    /// Registers the domain's services
    /// </summary>
    protected override void Load(ContainerBuilder builder)
    {
        // Build the database connection options
        var connectionStringBuilder = SqlConnectionStringBuilderHelper.DefaultSqlConnectionStringBuilder(_connectionString, _applicationName);

        var migrationsAssembly =
#if DEBUG
            typeof(WorkoutDesignTimeDbContextFactory).Assembly.FullName ?? string.Empty;
#else
        string.Empty;
#endif

        var optionsBuilder = SqlConnectionStringBuilderHelper.DefaultContextOptionsBuilder<WorkoutDbContext>(
            connectionStringBuilder.ConnectionString, WorkoutDbContext.SchemaName,
            _isProduction, migrationsAssembly);

        builder.RegisterInstance(optionsBuilder.Options);

        builder.RegisterAssemblyTypes(typeof(HandlerApplicationService).Assembly).AsSelf().AsImplementedInterfaces(); // Service layer
        builder.RegisterAssemblyTypes(typeof(WorkoutModule).Assembly).AsImplementedInterfaces(); // Application layer
    }

    public static void ApplyPolicies(Action<string, Action<AuthorizationPolicyBuilder>> addPolicyAction)
    {
        addPolicyAction(Constants.WorkoutIdPolicy, x => x.AddRequirements(new WorkoutIdRequirement()));
        addPolicyAction(Constants.RoutineIdPolicy, x => x.AddRequirements(new RoutineIdRequirement()));
        addPolicyAction(Constants.UserIdPolicy, x => x.AddRequirements(new UserIdRequirement()));
        addPolicyAction(Constants.SetIdPolicy, x => x.AddRequirements(new SetIdRequirement()));
    }
}
