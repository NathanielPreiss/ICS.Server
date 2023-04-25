namespace ICS.Workout;

public class WorkoutDbContext : DbContext
{
    public const string SchemaName = "Workout";

    public virtual DbSet<Routine> Routine { get; set; } = null!;
    public virtual DbSet<Set> Set { get; set; } = null!;
    public virtual DbSet<User> User { get; set; } = null!;
    public virtual DbSet<Workout> Workout { get; set; } = null!;

    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoutineConfig());
        modelBuilder.ApplyConfiguration(new SetConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new WorkoutConfig());
    }
}
