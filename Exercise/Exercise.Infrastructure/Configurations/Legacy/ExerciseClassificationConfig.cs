//namespace ICS.Exercise;

//public class ExerciseClassificationConfig : IEntityTypeConfiguration<ExerciseClassification>
//{
//    public void Configure(EntityTypeBuilder<ExerciseClassification> builder)
//    {
//        builder.ToTable(nameof(ExerciseClassification), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => x.ExerciseId);

//        builder.Property(x => x.ExerciseId).ValueGeneratedNever();
//        builder.Property(x => x.ForceId);
//        builder.Property(x => x.MechanicId);
//        builder.Property(x => x.UtilityId);

//        builder.HasOne<BodyArea>()
//            .WithMany()
//            .HasForeignKey(x => x.BodyAreaId)
//            .OnDelete(DeleteBehavior.Restrict);


//        builder.HasOne(x => x.Exercise).WithOne(x => 
//            x.ExerciseClassification).HasForeignKey<ExerciseClassification>(x => x.ExerciseId);
//        builder.HasOne(x => x.Force).WithMany().HasForeignKey(x => x.ForceId);
//        builder.HasOne(x => x.Mechanic).WithMany().HasForeignKey(x => x.MechanicId);
//        builder.HasOne(x => x.Utility).WithMany().HasForeignKey(x => x.UtilityId);

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<ExerciseClassification> GetData() => new ExerciseClassification[]
//    {
//        new(ExerciseTypes.ChestPress, ForceTypes.Push, MechanicTypes.Compound, UtilityTypes.Basic)
//    };
//}
