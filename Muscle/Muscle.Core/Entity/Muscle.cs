namespace ICS.Muscle;

public class Muscle
{
    public MuscleTypes MuscleId { get; }
    public string MuscleName { get; }
    public MuscleGroupTypes MuscleGroupId { get; }

    public MuscleGroup MuscleGroup => MuscleGroup.Lookup[MuscleGroupId];

    public static IReadOnlyDictionary<MuscleTypes, Muscle> Lookup { get; }
    public static IReadOnlyCollection<Muscle> Values { get; }

    internal static IReadOnlyDictionary<MuscleGroupTypes, IReadOnlyCollection<Muscle>> MuscleGroupMap { get; }

    static Muscle()
    {
        var valueList = ValueList();

        MuscleGroupMap = new ReadOnlyDictionary<MuscleGroupTypes, IReadOnlyCollection<Muscle>>(valueList
            .GroupBy(x => x.MuscleGroupId)
            .ToDictionary(x => x.Key,
                x => (IReadOnlyCollection<Muscle>)new ReadOnlyCollection<Muscle>(x.ToList())));

        Lookup = new ReadOnlyDictionary<MuscleTypes, Muscle>(valueList.ToDictionary(value => value.MuscleId));
        Values = new ReadOnlyCollection<Muscle>(valueList);
    }

    [JsonConstructor]
    private Muscle()
    {
        MuscleId = MuscleTypes.Invalid;
        MuscleName = string.Empty;
        MuscleGroupId = MuscleGroupTypes.Invalid;
    }

    private Muscle(MuscleTypes muscleId, string muscleName, MuscleGroupTypes muscleGroupId)
    {
        MuscleId = muscleId;
        MuscleName = muscleName;
        MuscleGroupId = muscleGroupId;
    }

    private static IList<Muscle> ValueList()
    {
        return new List<Muscle>
        {
            new(MuscleTypes.Sternocleidomastoid, "Sternocleidomastoid", MuscleGroupTypes.Neck),
            new(MuscleTypes.Splenius, "Splenius", MuscleGroupTypes.Neck),
            new(MuscleTypes.AnteriorDeltoid, "Anterior Deltoid", MuscleGroupTypes.Shoulder),
            new(MuscleTypes.LateralDeltoid, "Lateral Deltoid", MuscleGroupTypes.Shoulder),
            new(MuscleTypes.PosteriorDeltoid, "Posterior Deltoid", MuscleGroupTypes.Shoulder),
            new(MuscleTypes.Supraspinatus, "Supraspinatus", MuscleGroupTypes.Shoulder),
            new(MuscleTypes.PectoralisMajor, "Pectoralis Major", MuscleGroupTypes.Chest),
            new(MuscleTypes.PectoralisMinor, "Pectoralis Minor", MuscleGroupTypes.Chest),
            new(MuscleTypes.SerratusAnterior, "Serratus Anterior", MuscleGroupTypes.Chest),
            new(MuscleTypes.LatissimusDorsi, "Latissimus Dorsi", MuscleGroupTypes.UpperBack),
            new(MuscleTypes.Teres, "Teres", MuscleGroupTypes.UpperBack),
            new(MuscleTypes.Trapezius, "Trapezius", MuscleGroupTypes.UpperBack),
            new(MuscleTypes.LevatorScapulae, "Levator Scapulae", MuscleGroupTypes.UpperBack),
            new(MuscleTypes.Rhomboids, "Rhomboids", MuscleGroupTypes.UpperBack),
            new(MuscleTypes.Infraspinatus, "Infraspinatus", MuscleGroupTypes.UpperBack),
            new(MuscleTypes.Subscapularis, "Subscapularis", MuscleGroupTypes.UpperBack),
            new(MuscleTypes.TricepsBrachii, "Triceps Brachii", MuscleGroupTypes.UpperArm),
            new(MuscleTypes.BicepsBrachii, "Biceps Brachii", MuscleGroupTypes.UpperArm),
            new(MuscleTypes.Brachialis, "Brachialis", MuscleGroupTypes.UpperArm),
            new(MuscleTypes.Brachioradialis, "Brachioradialis", MuscleGroupTypes.Forearm),
            new(MuscleTypes.WristFlexors, "Wrist Flexors", MuscleGroupTypes.Forearm),
            new(MuscleTypes.WristExtendors, "Wrist Extendors", MuscleGroupTypes.Forearm),
            new(MuscleTypes.WristPronator, "Wrist Pronator", MuscleGroupTypes.Forearm),
            new(MuscleTypes.WristSupinator, "Wrist Supinator", MuscleGroupTypes.Forearm),
            new(MuscleTypes.QuadratusLumborum, "Quadratus Lumborum", MuscleGroupTypes.LowerBack),
            new(MuscleTypes.ErectorSpinae, "Erector Spinae", MuscleGroupTypes.LowerBack),
            new(MuscleTypes.RectusAbdominis, "Rectus Abdominis", MuscleGroupTypes.Abdomen),
            new(MuscleTypes.TransverseAbdominis, "Transverse Abdominis", MuscleGroupTypes.Abdomen),
            new(MuscleTypes.Obliques, "Obliques", MuscleGroupTypes.Abdomen),
            new(MuscleTypes.GluteusMaximus, "Gluteus Maximus", MuscleGroupTypes.Hip),
            new(MuscleTypes.Abductors, "Abductors", MuscleGroupTypes.Hip),
            new(MuscleTypes.Flexors, "Flexors", MuscleGroupTypes.Hip),
            new(MuscleTypes.Quadriceps, "Quadriceps", MuscleGroupTypes.Thigh),
            new(MuscleTypes.Hamstrings, "Hamstrings", MuscleGroupTypes.Thigh),
            new(MuscleTypes.Adductors, "Adductors", MuscleGroupTypes.Thigh),
            new(MuscleTypes.Gastrocnemius, "Gastrocnemius", MuscleGroupTypes.Calve),
            new(MuscleTypes.Soleus, "Soleus", MuscleGroupTypes.Calve),
            new(MuscleTypes.TibialisAnterior, "Tibialis Anterior", MuscleGroupTypes.Calve)
        };
    }
}
