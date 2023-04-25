//namespace ICS.Exercise;

//public class MuscleMap
//{
//    public MovementClassificationTypes MovementClassificationId { get; }
//    public MuscleTypes MuscleId { get; }

//    private MuscleMap(MovementClassificationTypes movementClassificationId, MuscleTypes muscleId)
//    {
//        MuscleId = muscleId;
//        MovementClassificationId = movementClassificationId;
//    }

//    public static readonly MuscleMap ChestPressTarget = new(MovementClassificationTypes.Target, MuscleTypes.PectoralisMajor);

//    public static readonly MuscleMap[] ChestPressSynergists =
//    {
//        new(MovementClassificationTypes.Synergist, MuscleTypes.PectoralisMinor),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Deltoid),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.TricepsBrachii)
//    };

//    public static readonly IEnumerable<MuscleMap> ChestPress = ChestPressSynergists.Prepend(ChestPressTarget);

//    public static readonly MuscleMap[] BenchPressStabilizers = { };

//    public static readonly MuscleMap[] PushUpStabilizers =
//    {
//        new(MovementClassificationTypes.Stabilizer, MuscleTypes.BicepsBrachii),
//        new(MovementClassificationTypes.Stabilizer, MuscleTypes.SerratusAnterior),
//        new(MovementClassificationTypes.Stabilizer, MuscleTypes.RectusAbdominis),
//        new(MovementClassificationTypes.Stabilizer, MuscleTypes.Obliques),
//        new(MovementClassificationTypes.Stabilizer, MuscleTypes.Quadriceps),
//        new(MovementClassificationTypes.Stabilizer, MuscleTypes.ErectorSpinae)
//    };





//    public static readonly MuscleMap[] NeckFlexion =
//    {
//        new(MovementClassificationTypes.Target, MuscleTypes.Sternocleidomastoid),
//        new(MovementClassificationTypes.Stabilizer, MuscleTypes.RectusAbdominis),
//        new(MovementClassificationTypes.Stabilizer, MuscleTypes.Obliques)
//    };

//    public static readonly MuscleMap[] NeckExtension =
//    {
//        new(MovementClassificationTypes.Target, MuscleTypes.Splenius),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Trapezius),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.LevatorScapulae),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.ErectorSpinae),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Sternocleidomastoid)
//    };

//    public static readonly MuscleMap[] FrontRaise =
//    {
//        new(MovementClassificationTypes.Target, MuscleTypes.AnteriorDeltoid),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.PectoralisMajor),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.LateralDeltoid),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Trapezius),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.SerratusAnterior)
//    };

//    public static readonly MuscleMap[] UprightRow =
//    {
//        new(MovementClassificationTypes.Target, MuscleTypes.LateralDeltoid),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.AnteriorDeltoid),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Supraspinatus),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Brachialis),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Brachioradialis),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.BicepsBrachii),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Trapezius),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.SerratusAnterior),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Infraspinatus),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Teres)
//    };

//    public static readonly MuscleMap[] RearRow =
//    {
//        new(MovementClassificationTypes.Target, MuscleTypes.PosteriorDeltoid),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Infraspinatus),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Teres),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.LateralDeltoid),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Trapezius),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Brachialis),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Brachioradialis),
//        new(MovementClassificationTypes.Synergist, MuscleTypes.Rhomboids)
//    };
//}
