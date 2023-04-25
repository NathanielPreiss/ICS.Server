namespace ICS.Equipment;

public class EquipmentDbContext : DbContext
{
    public const string SchemaName = "equipment";

    public EquipmentDbContext(DbContextOptions<EquipmentDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EquipmentGroupConfig());
    }
}
