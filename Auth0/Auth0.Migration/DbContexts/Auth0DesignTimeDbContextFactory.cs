namespace ICS.Auth0;

public class Auth0DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Auth0DbContext>
{
    public Auth0DbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Auth0DbContext>()
            .UseSqlServer(Constants.DebugConnectionString, x =>
            {
                x.MigrationsHistoryTable(Constants.EfMigrationTable, Auth0DbContext.SchemaName);
                x.MigrationsAssembly(GetType().Assembly.FullName);
            })
            .EnableSensitiveDataLogging();

        return new Auth0DbContext(optionsBuilder.Options);
    }
}
