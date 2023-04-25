////using ICS.Equipment;

//namespace ICS.Exercise;

//public class VariantClassification
//{
//    public VariantTypes VariantId { get; set; }
//    public string Description { get; set; }
//    public VisibilityTypes VisibilityId { get; set; }
//    public DifficultyTypes DifficultyId { get; set; }
//    //public EquipmentGroupTypes EquipmentGroupId { get; set; }

//    public Variant? Variant { get; set; }
//    public Visibility? Visibility { get; set; }
//    public Difficulty? Difficulty { get; set; }
//    //public EquipmentGroup? EquipmentGroup { get; set; }

//    [JsonConstructor]
//    private VariantClassification() : this(VariantTypes.Invalid, string.Empty, 
//        VisibilityTypes.Invalid, DifficultyTypes.Invalid/*, EquipmentGroupTypes.Invalid*/)
//    {
//    }

//    public VariantClassification(VariantTypes variantId, string description, VisibilityTypes visibilityId, 
//        DifficultyTypes difficultyId/*, EquipmentGroupTypes equipmentGroupId*/)
//    {
//        VariantId = variantId;
//        Description = description;
//        VisibilityId = visibilityId;
//        DifficultyId = difficultyId;
//        //EquipmentGroupId = equipmentGroupId;

//        Variant = null;
//        Visibility = null;
//        Difficulty = null;
//        //EquipmentGroup = null;
//    }
//}
