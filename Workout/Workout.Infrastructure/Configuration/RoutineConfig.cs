namespace ICS.Workout;

public class RoutineConfig : IEntityTypeConfiguration<Routine>
{
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        builder.ToTable(nameof(Routine), WorkoutDbContext.SchemaName);
        builder.HasKey(x => x.RoutineId).IsClustered(false);
        builder.HasIndex(x => new { x.WorkoutId, x.Position }).IsClustered();
        builder.HasAlternateKey(x => new { x.RoutineId, x.Position });

        builder.Property(x => x.RoutineId);
        builder.Property(x => x.WorkoutId);
        builder.Property(x => x.Position);
        builder.Property(x => x.ExerciseId);

        builder.HasOne(x => x.Workout)
            .WithMany(x => x.Routines)
            .HasForeignKey(x => x.WorkoutId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}