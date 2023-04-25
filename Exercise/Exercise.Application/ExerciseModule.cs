namespace ICS.Exercise;

public class ExerciseModule : Module
{
    private readonly string _connectionString;
    private readonly string _applicationName;
    private readonly bool _isProduction;

    public ExerciseModule(string applicationName, bool isProduction, string connectionString)
    {
        _applicationName = applicationName;
        _isProduction = isProduction;
        _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
        // Build the database connection options
        var connectionStringBuilder = SqlConnectionStringBuilderHelper.DefaultSqlConnectionStringBuilder(_connectionString, _applicationName);

        var migrationsAssembly =
#if DEBUG
            typeof(ExerciseDesignTimeDbContextFactory).Assembly.FullName ?? string.Empty;
#else
        string.Empty;
#endif

        var optionsBuilder = SqlConnectionStringBuilderHelper.DefaultContextOptionsBuilder<ExerciseDbContext>(
            connectionStringBuilder.ConnectionString, ExerciseDbContext.SchemaName,
            _isProduction, migrationsAssembly);

        builder.RegisterInstance(optionsBuilder.Options);

        builder.RegisterAssemblyTypes(typeof(ApiApplicationService).Assembly).AsSelf().AsImplementedInterfaces(); // Service layer
        builder.RegisterAssemblyTypes(typeof(ExerciseModule).Assembly).AsSelf().AsImplementedInterfaces(); // Applications layer
    }
}
