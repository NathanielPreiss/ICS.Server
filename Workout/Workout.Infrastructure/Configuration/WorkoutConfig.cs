namespace ICS.Workout;

public class WorkoutConfig : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.ToTable(nameof(Workout), WorkoutDbContext.SchemaName);
        builder.HasKey(x => x.WorkoutId).IsClustered(false);
        builder.HasIndex(x => new { x.UserId, x.Position }).IsClustered();
        builder.HasAlternateKey(x => new { x.WorkoutId, x.Position });

        builder.Property(x => x.WorkoutId);
        builder.Property(x => x.UserId);
        builder.Property(x => x.Position);
        builder.Property(x => x.Name);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Workouts)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}