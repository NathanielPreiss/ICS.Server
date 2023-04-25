//namespace ICS.Exercise;

//public class EquipmentGroupConfig : IEntityTypeConfiguration<EquipmentGroup>
//{
//    public void Configure(EntityTypeBuilder<EquipmentGroup> builder)
//    {
        
//        builder.ToTable(nameof(Muscle), ExerciseDbContext.SchemaName);
//        builder.HasKey(x => x.EquipmentGroupId);

//        builder.Property(x => x.EquipmentGroupId).ValueGeneratedNever();
//        builder.Property(x => x.EquipmentGroupName).IsRequired().HasMaxLength(Constants.NameLength);

//        builder.HasData(GetData());
//    }

//    private static IEnumerable<EquipmentGroup> GetData() =>
//        ICS.Equipment.EquipmentGroupConfig.GetData().Select(x => new EquipmentGroup(x.EquipmentGroupId, x.EquipmentGroupName));
//}