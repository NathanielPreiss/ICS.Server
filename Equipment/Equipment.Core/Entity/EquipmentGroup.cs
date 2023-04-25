namespace ICS.Equipment;

public class EquipmentGroup
{
    public EquipmentGroupTypes EquipmentGroupId { get; }
    public string EquipmentGroupName { get; }

    public static IReadOnlyDictionary<EquipmentGroupTypes, EquipmentGroup> Lookup { get; }
    public static IReadOnlyCollection<EquipmentGroup> Values { get; }

    static EquipmentGroup()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<EquipmentGroupTypes, EquipmentGroup>(valueList.ToDictionary(value => value.EquipmentGroupId));
        Values = new ReadOnlyCollection<EquipmentGroup>(valueList);
    }

    private EquipmentGroup(EquipmentGroupTypes equipmentGroupId, string equipmentGroupName)
    {
        EquipmentGroupId = equipmentGroupId;
        EquipmentGroupName = equipmentGroupName;
    }

    private static IList<EquipmentGroup> ValueList()
    {
        return new List<EquipmentGroup>
        {
            new(EquipmentGroupTypes.Barbell, "Barbell"),
            new(EquipmentGroupTypes.Cable, "Cable"),
            new(EquipmentGroupTypes.Dumbbell, "Dumbbell"),
            new(EquipmentGroupTypes.Lever, "Lever"),
            new(EquipmentGroupTypes.Sled, "Sled"),
            new(EquipmentGroupTypes.Smith, "Smith"),
            new(EquipmentGroupTypes.Band, "Band"),
            new(EquipmentGroupTypes.Suspension, "Suspension"),
            new(EquipmentGroupTypes.Calisthenics, "Calisthenics")
        };
    }
}
