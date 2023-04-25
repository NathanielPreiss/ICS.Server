//namespace ICS.Exercise;

//public class ForceConfig : IEntityTypeConfiguration<Force>
//{
//    public void Configure(EntityTypeBuilder<Force> builder)
//    {
//        builder.ToTable(nameof(Force), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => x.ForceId);

//        builder.Property(x => x.ForceId).ValueGeneratedNever();
//        builder.Property(x => x.ForceName).HasMaxLength(256);

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<Force> GetData() => new Force[]
//    {
//        new(ForceTypes.Push, "Push"),
//        new(ForceTypes.Pull, "Pull")
//    };
//}
