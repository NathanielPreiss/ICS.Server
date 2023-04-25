namespace ICS.Exercise;

public class ClassificationConfig : IEntityTypeConfiguration<Classification>
{
    public void Configure(EntityTypeBuilder<Classification> builder)
    {
        
        builder.ToTable(nameof(Classification), ExerciseDbContext.SchemaName);
        builder.HasKey(x => x.ExerciseId);

        builder.Property(x => x.ExerciseId).ValueGeneratedNever();
        builder.Property(x => x.MechanicId).IsRequired();
        builder.Property(x => x.UtilityId).IsRequired();

        builder.HasOne<Exercise>()
            .WithOne()
            .HasForeignKey<Classification>(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(Classification.Values);
    }
}