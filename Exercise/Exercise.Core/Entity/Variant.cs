//namespace ICS.Exercise;

//public class Variant
//{
//    public VariantTypes VariantId { get; }
//    public ExerciseTypes ExerciseId { get; }
//    public string VariantName { get; }
//    public UtilityTypes UtilityId { get; }
//    public MechanicTypes MechanicId { get; }
//    public ForceTypes ForceId { get; }
//    public DifficultyTypes DifficultyId { get; }

//    //public EquipmentGroupTypes EquipmentGroupId { get; }
//    //public VisibilityTypes VisibilityId { get; }
//    //public DifficultyTypes DifficultyId { get; }

//    //public IEnumerable<MuscleMap> MuscleMaps { get; }

//    private Variant(VariantTypes variantId, ExerciseTypes exerciseId, string variantName /*, EquipmentGroupTypes equipmentGroupId, VisibilityTypes visibilityId, DifficultyTypes difficultyId, IEnumerable<MuscleMap> muscleMaps*/)
//    {
//        VariantId = variantId;
//        ExerciseId = exerciseId;
//        VariantName = variantName;
//        //VisibilityId = visibilityId;
//        //DifficultyId = difficultyId;
//        //MuscleMaps = muscleMaps;
//    }

//    private Variant(VariantTypes variantId, EquipmentGroupTypes equipmentGroupId, VisibilityTypes visibilityId, DifficultyTypes difficultyId, IEnumerable<MuscleMap> muscleMaps)
//    {
//        VariantId = variantId;
//        EquipmentGroupId = equipmentGroupId;
//        VisibilityId = visibilityId;
//        DifficultyId = difficultyId;
//        MuscleMaps = muscleMaps;
//    }

//    private Variant(ExerciseTypes exerciseId, EquipmentGroupTypes equipmentGroupId, VisibilityTypes visibilityId, DifficultyTypes difficultyId) :
//        this(exerciseId, equipmentGroupId, visibilityId, difficultyId, Array.Empty<MuscleMap>())
//    {
//    }

//    public static readonly Variant[] ChestPress =
//    {
//        new(VariantTypes.BenchPress, ExerciseTypes.ChestPress, "Bench Press"),
//        new(VariantTypes.PushUp, ExerciseTypes.ChestPress, "Push Up"),
//    };

//    /*public static readonly Variant[] NeckFlexion =
//    {
//        new(ExerciseTypes.NeckFlexion, EquipmentGroupTypes.Cable, VisibilityTypes.Default, DifficultyTypes.Beginner),
//        new(ExerciseTypes.NeckFlexion, EquipmentGroupTypes.Lever, VisibilityTypes.Default, DifficultyTypes.Beginner),
//        new(ExerciseTypes.NeckFlexion, EquipmentGroupTypes.Band, VisibilityTypes.Default, DifficultyTypes.Beginner)
//    };

//    public static readonly Variant[] NeckExtension =
//    {
//        new(ExerciseTypes.NeckExtension, EquipmentGroupTypes.Cable, VisibilityTypes.Default, DifficultyTypes.Beginner),
//        new(ExerciseTypes.NeckExtension, EquipmentGroupTypes.Lever, VisibilityTypes.Default, DifficultyTypes.Beginner),
//        new(ExerciseTypes.NeckExtension, EquipmentGroupTypes.Band, VisibilityTypes.Default, DifficultyTypes.Beginner)
//    };*/
//}
