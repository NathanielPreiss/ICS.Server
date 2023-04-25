namespace ICS.Exercise;

public class ExerciseDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExerciseDbContext>
{
    public ExerciseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ExerciseDbContext>()
            .UseSqlServer(Constants.DebugConnectionString, x =>
            {
                x.MigrationsHistoryTable(Constants.EfMigrationTable, ExerciseDbContext.SchemaName);
                x.MigrationsAssembly(GetType().Assembly.FullName);
            })
            .EnableSensitiveDataLogging();

        return new ExerciseDbContext(optionsBuilder.Options);
    }
}
