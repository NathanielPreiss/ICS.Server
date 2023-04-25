namespace ICS.Exercise;

public class ExerciseDbContext : DbContext
{
    public const string SchemaName = "exercise";

    public virtual DbSet<Exercise> Exercise { get; set; } = null!;
    //public virtual DbSet<Variant> Variant { get; set; } = null!;

    public ExerciseDbContext(DbContextOptions<ExerciseDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new DifficultyConfig());
        modelBuilder.ApplyConfiguration(new ClassificationConfig());
        modelBuilder.ApplyConfiguration(new ExerciseConfig());
        //modelBuilder.ApplyConfiguration(new ExerciseMuscleMapConfig());
        //modelBuilder.ApplyConfiguration(new ForceConfig());
        modelBuilder.ApplyConfiguration(new MechanicConfig());
        //modelBuilder.ApplyConfiguration(new MovementClassificationConfig());
        modelBuilder.ApplyConfiguration(new MuscleConfig());
        modelBuilder.ApplyConfiguration(new UtilityConfig());
        //modelBuilder.ApplyConfiguration(new VariantConfig());
        //modelBuilder.ApplyConfiguration(new VariantClassificationConfig());
        //modelBuilder.ApplyConfiguration(new VisibilityConfig());
    }
}
