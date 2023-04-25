namespace ICS.Muscle;

public class MuscleConfig : IEntityTypeConfiguration<Muscle>
{
    public void Configure(EntityTypeBuilder<Muscle> builder)
    {
        
        builder.ToTable(nameof(Muscle), MuscleDbContext.SchemaName);
        builder.HasKey(x => x.MuscleId);

        builder.Property(x => x.MuscleId).ValueGeneratedNever();
        builder.Property(x => x.MuscleGroupId).ValueGeneratedNever();
        builder.Property(x => x.MuscleName).IsRequired().HasMaxLength(Constants.NameLength);

        builder.HasOne<MuscleGroup>()
            .WithMany()
            .HasForeignKey(x => x.MuscleGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(Muscle.Values);
    }
}