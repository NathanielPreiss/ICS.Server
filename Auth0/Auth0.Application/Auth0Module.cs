namespace ICS.Auth0;

public class Auth0Module : Module
{
    private readonly string _connectionString;
    private readonly string _applicationName;
    private readonly bool _isProduction;

    public Auth0Module(string applicationName, bool isProduction, string connectionString)
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
            typeof(Auth0DesignTimeDbContextFactory).Assembly.FullName ?? string.Empty;
#else
        string.Empty;
#endif

        var optionsBuilder = SqlConnectionStringBuilderHelper.DefaultContextOptionsBuilder<Auth0DbContext>(
            connectionStringBuilder.ConnectionString, Auth0DbContext.SchemaName,
            _isProduction, migrationsAssembly);

        builder.RegisterInstance(optionsBuilder.Options);

        builder.RegisterAssemblyTypes(typeof(UtilityService).Assembly).AsSelf().AsImplementedInterfaces(); // Service layer
        builder.RegisterAssemblyTypes(typeof(Auth0Module).Assembly).AsSelf().AsImplementedInterfaces(); // Application layer
    }
}
