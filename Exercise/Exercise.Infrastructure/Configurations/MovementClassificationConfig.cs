//namespace ICS.Exercise;

//public class MovementClassificationConfig : IEntityTypeConfiguration<MovementClassification>
//{
//    public void Configure(EntityTypeBuilder<MovementClassification> builder)
//    {
//        builder.ToTable(nameof(MovementClassification), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => x.MovementClassificationId);

//        builder.Property(x => x.MovementClassificationId).ValueGeneratedNever();
//        builder.Property(x => x.MovementClassificationName);

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<MovementClassification> GetData() => new MovementClassification[]
//    {
//        new(MovementClassificationTypes.Agonist, "Agonist"),
//        new(MovementClassificationTypes.Antagonist, "Antagonist"),
//        new(MovementClassificationTypes.Target, "Target"),
//        new(MovementClassificationTypes.Synergist, "Synergist"),
//        new(MovementClassificationTypes.Stabilizer, "Stabilizer")
//    };
//}
