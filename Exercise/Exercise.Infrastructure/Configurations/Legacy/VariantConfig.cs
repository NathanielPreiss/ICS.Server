//namespace ICS.Exercise;

//public class VariantConfig : IEntityTypeConfiguration<Variant>
//{
//    public void Configure(EntityTypeBuilder<Variant> builder)
//    {
//        builder.ToTable(nameof(Variant), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => x.VariantId);

//        builder.Property(x => x.VariantId).ValueGeneratedNever();
//        builder.Property(x => x.ExerciseId);
//        builder.Property(x => x.VariantName).HasMaxLength(256);

//        builder.HasOne(x => x.Exercise).WithMany(x => x.Variants).HasForeignKey(x => x.ExerciseId);

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<Variant> GetData() => new Variant[]
//    {
//        new(VariantTypes.BenchPress, ExerciseTypes.ChestPress, "Bench Press")
//    };
//}
