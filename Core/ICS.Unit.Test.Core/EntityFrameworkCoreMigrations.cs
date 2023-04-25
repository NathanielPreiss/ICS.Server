namespace ICS;

public static class EntityFrameworkCoreMigrations
{
    public static void CheckForPendingDbContextSchemaChanges<T1>(this T1 dbContextFactory)
        where T1 : IDesignTimeDbContextFactory<DbContext>

    {
        var ctx = dbContextFactory.CreateDbContext(Array.Empty<string>());

        var migrationsAssembly = ctx.GetService<IMigrationsAssembly>();

        var hasDifferences = false;
        if (migrationsAssembly.ModelSnapshot != null)
        {
            var snapshotModel = migrationsAssembly.ModelSnapshot?.Model;

            if (snapshotModel is IMutableModel mutableModel)
            {
                snapshotModel = mutableModel.FinalizeModel();
            }

            Assert.IsNotNull(snapshotModel);

            snapshotModel = ctx.GetService<IModelRuntimeInitializer>().Initialize(snapshotModel);
            hasDifferences = ctx.GetService<IMigrationsModelDiffer>().HasDifferences(
                snapshotModel.GetRelationalModel(),
                ctx.GetService<IDesignTimeModel>().Model.GetRelationalModel());
        }

        Assert.IsFalse(hasDifferences, $"There are pending changes to the {ctx.GetType()} database context.");
    }
}
