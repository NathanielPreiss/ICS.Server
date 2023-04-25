namespace ICS.Muscle;

public class JointMuscleGroupMapConfig : IEntityTypeConfiguration<JointMuscleGroupMap>
{
    public void Configure(EntityTypeBuilder<JointMuscleGroupMap> builder)
    {
        builder.ToTable(nameof(JointMuscleGroupMap), MuscleDbContext.SchemaName);
        builder.HasKey(x => new {x.JointId, x.MuscleGroupId});

        builder.Property(x => x.JointId).ValueGeneratedNever();
        builder.Property(x => x.MuscleGroupId).ValueGeneratedNever();

        builder.HasOne<Joint>()
            .WithMany()
            .HasForeignKey(x => x.JointId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne<MuscleGroup>()
            .WithMany()
            .HasForeignKey(x => x.MuscleGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(JointMuscleGroupMap.Values);
    }
}