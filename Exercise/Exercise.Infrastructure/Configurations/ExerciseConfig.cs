namespace ICS.Exercise;

public class ExerciseConfig : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable(nameof(Exercise), ExerciseDbContext.SchemaName);
        builder.HasKey(x => x.ExerciseId);

        builder.Property(x => x.ExerciseId).ValueGeneratedNever();
        builder.Property(x => x.ExerciseName).HasMaxLength(256);

        builder.HasData(Exercise.Values);
    }
}
