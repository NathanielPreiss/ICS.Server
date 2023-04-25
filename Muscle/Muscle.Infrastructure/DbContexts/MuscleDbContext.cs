namespace ICS.Muscle;

public class MuscleDbContext : DbContext
{
    public const string SchemaName = "muscle";

    public MuscleDbContext(DbContextOptions<MuscleDbContext> options) : base(options)
    {
    } 

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BodyAreaConfig());
        modelBuilder.ApplyConfiguration(new JointConfig());
        modelBuilder.ApplyConfiguration(new JointMuscleGroupMapConfig());
        modelBuilder.ApplyConfiguration(new MuscleConfig());
        modelBuilder.ApplyConfiguration(new MuscleGroupConfig());
    }
}