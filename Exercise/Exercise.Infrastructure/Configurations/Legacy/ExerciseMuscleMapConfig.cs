//namespace ICS.Exercise;

//public class ExerciseMuscleMapConfig : IEntityTypeConfiguration<ExerciseMuscleMap>
//{
//    public void Configure(EntityTypeBuilder<ExerciseMuscleMap> builder)
//    {
//        builder.ToTable(nameof(ExerciseMuscleMap), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => new { x.ExerciseId, x.MuscleId });

//        builder.Property(x => x.ExerciseId).ValueGeneratedNever();
//        builder.Property(x => x.MuscleId).ValueGeneratedNever();
//        builder.Property(x => x.MovementClassificationId);

//        builder.HasOne(x => x.Exercise).WithMany().HasForeignKey(x => x.ExerciseId);
//        builder.HasOne(x => x.Muscle).WithMany().HasForeignKey(x => x.MuscleId);
//        builder.HasOne(x => x.MovementClassification).WithMany()
//            .HasForeignKey(x => x.MovementClassificationId).OnDelete(DeleteBehavior.NoAction);

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<ExerciseMuscleMap> GetData() => new ExerciseMuscleMap[]
//    {
//        new(ExerciseTypes.ChestPress, MuscleTypes.PectoralisMajor, MovementClassificationTypes.Target),

//        new(ExerciseTypes.ChestPress, MuscleTypes.PectoralisMinor, MovementClassificationTypes.Synergist),
//        new(ExerciseTypes.ChestPress, MuscleTypes.Deltoid, MovementClassificationTypes.Synergist),
//        new(ExerciseTypes.ChestPress, MuscleTypes.TricepsBrachii, MovementClassificationTypes.Synergist),

//        new(ExerciseTypes.ChestPress, MuscleTypes.BicepsBrachii, MovementClassificationTypes.Stabilizer)
//    };
//}
