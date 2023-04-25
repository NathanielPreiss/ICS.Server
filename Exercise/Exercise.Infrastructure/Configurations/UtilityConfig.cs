namespace ICS.Exercise;

public class UtilityConfig : IEntityTypeConfiguration<Utility>
{
    public void Configure(EntityTypeBuilder<Utility> builder)
    {
        builder.ToTable(nameof(Utility), ExerciseDbContext.SchemaName);
        builder.HasKey(x => x.UtilityId);

        builder.Property(x => x.UtilityId).ValueGeneratedNever();
        builder.Property(x => x.UtilityName).HasMaxLength(256);

        builder.HasData(Utility.Values);
    }
}
