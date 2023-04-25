namespace ICS.Exercise;

public class MuscleEngagementConfig : IEntityTypeConfiguration<MuscleEngagement>
{
    public void Configure(EntityTypeBuilder<MuscleEngagement> builder)
    {
        builder.ToTable(nameof(MuscleEngagement), ExerciseDbContext.SchemaName);
        builder.HasKey(x => x.MuscleEngagementId);

        builder.Property(x => x.MuscleEngagementId).ValueGeneratedNever();
        builder.Property(x => x.MuscleEngagementName).HasMaxLength(Constants.NameLength);

        builder.HasData(MuscleEngagement.Values);
    }
}
