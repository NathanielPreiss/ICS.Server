//namespace ICS.Exercise;

//public class VariantClassificationConfig : IEntityTypeConfiguration<VariantClassification>
//{
//    public void Configure(EntityTypeBuilder<VariantClassification> builder)
//    {
//        builder.ToTable(nameof(VariantClassification), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => x.VariantId);

//        builder.Property(x => x.VariantId).ValueGeneratedNever();
//        builder.Property(x => x.Description).HasMaxLength(512);
//        builder.Property(x => x.VisibilityId);
//        builder.Property(x => x.DifficultyId);
//        //builder.Property(x => x.EquipmentGroupId);

//        builder.HasOne(x => x.Variant).WithOne(x => x.VariantClassification).HasForeignKey<VariantClassification>(x => x.VariantId);
//        builder.HasOne(x => x.Visibility).WithMany().HasForeignKey(x => x.VisibilityId);
//        builder.HasOne(x => x.Difficulty).WithMany().HasForeignKey(x => x.DifficultyId);
//        //builder.HasOne(x => x.EquipmentGroup).WithMany().HasForeignKey(x => x.EquipmentGroupId);

//        // This would only work if visibility was on Variants because it needs to be against the ExerciseId
//        //builder.HasIndex(x => new {x.VariantId, x.VisibilityId}).IsUnique()
//        //    .HasFilter($"{nameof(VariantClassification.VariantId)} = {VisibilityTypes.Default}");

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<VariantClassification> GetData() => new VariantClassification[]
//    {
//        //new(VariantTypes.BenchPress, "A commonly used exercise to increase chest strength.", VisibilityTypes.Default, DifficultyTypes.Beginner, EquipmentGroupTypes.Barbell)
//    };
//}
