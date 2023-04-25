namespace ICS.Muscle;

public class JointConfig : IEntityTypeConfiguration<Joint>
{
    public void Configure(EntityTypeBuilder<Joint> builder)
    {
        builder.ToTable(nameof(Joint), MuscleDbContext.SchemaName);
        builder.HasKey(x => x.JointId);

        builder.Property(x => x.JointId).ValueGeneratedNever();
        builder.Property(x => x.JointName).IsRequired().HasMaxLength(Constants.NameLength);

        builder.Ignore(x => x.MuscleGroups);

        builder.HasData(Joint.Values);
    }
}