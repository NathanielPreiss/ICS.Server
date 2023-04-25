namespace ICS.Equipment;

public class EquipmentGroupConfig : IEntityTypeConfiguration<EquipmentGroup>
{
    public void Configure(EntityTypeBuilder<EquipmentGroup> builder)
    {
        builder.ToTable(nameof(EquipmentGroup), EquipmentDbContext.SchemaName);
        builder.HasKey(x => x.EquipmentGroupId);

        builder.Property(x => x.EquipmentGroupId).ValueGeneratedNever();
        builder.Property(x => x.EquipmentGroupName).HasMaxLength(Constants.NameLength);

        builder.HasData(EquipmentGroup.Values);
    }
}
