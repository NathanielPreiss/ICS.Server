namespace ICS;

public static class SqlConnectionStringBuilderHelper
{
    /// <summary>
    /// Creates a connection string builder for the Widget database with default settings.
    /// </summary>
    public static SqlConnectionStringBuilder DefaultSqlConnectionStringBuilder(string connectionString, string applicationName)
    {
        return new(connectionString)
        {
            ApplicationName = applicationName,
            ApplicationIntent = ApplicationIntent.ReadWrite,
            ConnectTimeout = 30,
            Encrypt = true,
            MultipleActiveResultSets = false,
            PersistSecurityInfo = false,
            TrustServerCertificate = true,
            WorkstationID = Environment.MachineName
        };
    }

    public static DbContextOptionsBuilder<T> DefaultContextOptionsBuilder<T>(string connectionString, string schemaName, bool isProduction, string designTimeFactoryAssembly) where T : DbContext
    {
        var optionsBuilder = new DbContextOptionsBuilder<T>()
            .UseSqlServer(connectionString, x =>
            {
                x.MigrationsHistoryTable(Constants.EfMigrationTable, schemaName);

                // When in debug we need to include the migration assembly since it's a different project.
                // In debug startup will attempt to automatically migrate the databases and without the assembly nothing will actually migrate.
                if (!string.IsNullOrEmpty(designTimeFactoryAssembly))
                {
                    x.MigrationsAssembly(designTimeFactoryAssembly);
                }
            });

        // Add debugging options if not in production
        if (!isProduction)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        return optionsBuilder;
    }
}
