namespace ICS.Equipment;

public static class EquipmentGroupResourceMapping
{
    public static string Name(this EquipmentGroupTypes equipmentGroupId) =>
        Resources.EquipmentGroup_Name.ResourceManager.GetString($"{equipmentGroupId}") ??
               throw new MissingResourceMappingException(nameof(Name), nameof(equipmentGroupId), equipmentGroupId);

    public static string Description(this EquipmentGroupTypes equipmentGroupId) =>
        Resources.EquipmentGroup_Description.ResourceManager.GetString($"{equipmentGroupId}") ??
               throw new MissingResourceMappingException(nameof(Description), nameof(equipmentGroupId), equipmentGroupId);
}
