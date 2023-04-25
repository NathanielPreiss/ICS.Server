namespace ICS.Equipment;

public class EquipmentDesignTimeDbContextFactory : IDesignTimeDbContextFactory<EquipmentDbContext>
{
    public EquipmentDbContext CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<EquipmentDbContext>()
            .UseSqlServer(Constants.DebugConnectionString, x =>
            {
                x.MigrationsHistoryTable(Constants.EfMigrationTable, EquipmentDbContext.SchemaName);
                x.MigrationsAssembly(GetType().Assembly.FullName);
            })
            .EnableSensitiveDataLogging();

        return new EquipmentDbContext(optionsBuilder.Options);
    }
}
