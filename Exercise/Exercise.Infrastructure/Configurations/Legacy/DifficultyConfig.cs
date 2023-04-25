//namespace ICS.Exercise;

//public class DifficultyConfig : IEntityTypeConfiguration<Difficulty>
//{
//    public void Configure(EntityTypeBuilder<Difficulty> builder)
//    {
//        builder.ToTable(nameof(Difficulty), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => x.DifficultyId);

//        builder.Property(x => x.DifficultyId).ValueGeneratedNever();
//        builder.Property(x => x.DifficultyName).HasMaxLength(256);

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<Difficulty> GetData() => new Difficulty[] 
//    {
//        new(DifficultyTypes.Beginner, "Beginner"),
//        new(DifficultyTypes.Experienced, "Experienced"),
//        new(DifficultyTypes.Advanced, "Advanced")
//    };
//}
