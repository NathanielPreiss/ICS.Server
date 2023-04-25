namespace ICS.Muscle;

public class BodyAreaConfig : IEntityTypeConfiguration<BodyArea>
{
    public void Configure(EntityTypeBuilder<BodyArea> builder)
    {
        builder.ToTable(nameof(BodyArea), MuscleDbContext.SchemaName);
        builder.HasKey(x => x.BodyAreaId);

        builder.Property(x => x.BodyAreaId).ValueGeneratedNever();
        builder.Property(x => x.BodyAreaName).IsRequired().HasMaxLength(Constants.NameLength);

        builder.Ignore(x => x.MuscleGroups);

        builder.HasData(BodyArea.Values);
    }
}