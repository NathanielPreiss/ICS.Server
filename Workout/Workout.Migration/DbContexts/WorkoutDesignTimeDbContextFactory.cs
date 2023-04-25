namespace ICS.Workout;

public class WorkoutDesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkoutDbContext>
{
    public WorkoutDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WorkoutDbContext>()
            .UseSqlServer(ICS.Constants.DebugConnectionString, x =>
            {
                x.MigrationsHistoryTable(ICS.Constants.EfMigrationTable, WorkoutDbContext.SchemaName);
                x.MigrationsAssembly(GetType().Assembly.FullName);
            })
            .EnableSensitiveDataLogging();

        return new WorkoutDbContext(optionsBuilder.Options);
    }
}
