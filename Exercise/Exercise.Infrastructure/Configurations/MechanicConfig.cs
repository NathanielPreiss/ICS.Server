namespace ICS.Exercise;

public class MechanicConfig : IEntityTypeConfiguration<Mechanic>
{
    public void Configure(EntityTypeBuilder<Mechanic> builder)
    {
        builder.ToTable(nameof(Mechanic), ExerciseDbContext.SchemaName);
        builder.HasKey(x => x.MechanicId);

        builder.Property(x => x.MechanicId).ValueGeneratedNever();
        builder.Property(x => x.MechanicName).HasMaxLength(256);

        builder.HasData(Mechanic.Values);
    }
}
