//namespace ICS.Exercise;

//public class VisibilityConfig : IEntityTypeConfiguration<Visibility>
//{
//    public void Configure(EntityTypeBuilder<Visibility> builder)
//    {
//        builder.ToTable(nameof(Visibility), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => x.VisibilityId);

//        builder.Property(x => x.VisibilityId).ValueGeneratedNever();
//        builder.Property(x => x.VisibilityName).HasMaxLength(256);

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<Visibility> GetData() => new Visibility[]
//    {
//        new(VisibilityTypes.Default, "Default"),
//        new(VisibilityTypes.High, "High"),
//        new(VisibilityTypes.Uncommon, "Uncommon"),
//        new(VisibilityTypes.Hidden, "Hidden")
//    };
//}
