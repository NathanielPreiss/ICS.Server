namespace ICS.Muscle;

public class MuscleDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MuscleDbContext>
{
    public MuscleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MuscleDbContext>()
            .UseSqlServer(Constants.DebugConnectionString, x =>
            {
                x.MigrationsHistoryTable(Constants.EfMigrationTable, MuscleDbContext.SchemaName);
                x.MigrationsAssembly(GetType().Assembly.FullName);
            })
            .EnableSensitiveDataLogging();

        return new MuscleDbContext(optionsBuilder.Options);
    }
}
