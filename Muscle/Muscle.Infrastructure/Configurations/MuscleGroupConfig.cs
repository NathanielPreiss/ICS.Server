namespace ICS.Muscle;

public class MuscleGroupConfig : IEntityTypeConfiguration<MuscleGroup>
{
    public void Configure(EntityTypeBuilder<MuscleGroup> builder)
    {
        builder.ToTable(nameof(MuscleGroup), MuscleDbContext.SchemaName);
        builder.HasKey(x => x.MuscleGroupId);

        builder.Property(x => x.MuscleGroupId).ValueGeneratedNever();
        builder.Property(x => x.BodyAreaId);
        builder.Property(x => x.MuscleGroupName).IsRequired().HasMaxLength(Constants.NameLength);

        builder.HasOne<BodyArea>()
            .WithMany()
            .HasForeignKey(x => x.BodyAreaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(x => x.Joints);
        builder.Ignore(x => x.Muscles);

        builder.HasData(MuscleGroup.Values);
    }
}