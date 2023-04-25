namespace ICS.Exercise;

public class MuscleConfig : IEntityTypeConfiguration<Muscle.Muscle>
{
    public void Configure(EntityTypeBuilder<Muscle.Muscle> builder)
    {
        
        builder.ToTable(nameof(Muscle), ExerciseDbContext.SchemaName);
        builder.HasKey(x => x.MuscleId);

        builder.Property(x => x.MuscleId).ValueGeneratedNever();
        builder.Property(x => x.MuscleName).IsRequired().HasMaxLength(Constants.NameLength);

        builder.HasData(Muscle.Muscle.Values);
    }
}