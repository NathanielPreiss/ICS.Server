namespace ICS.Workout;

public class SetConfig : IEntityTypeConfiguration<Set>
{
    public void Configure(EntityTypeBuilder<Set> builder)
    {
        builder.ToTable(nameof(Set), WorkoutDbContext.SchemaName);
        builder.HasKey(x => x.SetId).IsClustered(false);
        builder.HasIndex(x => new { x.RoutineId, x.Position }).IsClustered();
        builder.HasAlternateKey(x => new { x.SetId, x.Position });

        builder.Property(x => x.SetId);
        builder.Property(x => x.RoutineId);
        builder.Property(x => x.Position);
        builder.Property(x => x.Reps);
        builder.Property(x => x.Weight);

        builder.HasOne(x => x.Routine)
            .WithMany(x => x.Sets)
            .HasForeignKey(x => x.RoutineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}