namespace ICS.Equipment;

public class EquipmentGroupDto
{
    public EquipmentGroupTypes EquipmentGroupId { get; set; }
    public string Name { get; set; }
    
    public string Description { get; set; }

    public EquipmentGroupDto(EquipmentGroupTypes equipmentGroupId, string name, string description)
    {
        EquipmentGroupId = equipmentGroupId;
        Name = name;
        Description = description;
    }
}