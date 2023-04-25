namespace ICS.Muscle;

public class JointMuscleGroupMap
{
    public JointTypes JointId { get; }
    public MuscleGroupTypes MuscleGroupId { get; }

    public Joint Joint => Joint.Lookup[JointId];

    public MuscleGroup MuscleGroup => MuscleGroup.Lookup[MuscleGroupId];

    static JointMuscleGroupMap()
    {
        var valueList = ValueList();

        MuscleGroupMap = new ReadOnlyDictionary<MuscleGroupTypes, IReadOnlyCollection<Joint>>(valueList
            .GroupBy(x => x.MuscleGroupId)
            .ToDictionary(x => x.Key,
                x => (IReadOnlyCollection<Joint>)new ReadOnlyCollection<Joint>(x.Select(x1 => x1.Joint).ToList())));

        JointMap = new ReadOnlyDictionary<JointTypes, IReadOnlyCollection<MuscleGroup>>(valueList
            .GroupBy(x => x.JointId)
            .ToDictionary(x => x.Key,
                x => (IReadOnlyCollection<MuscleGroup>)new ReadOnlyCollection<MuscleGroup>(x.Select(x1 => x1.MuscleGroup).ToList())));

        Values = new ReadOnlyCollection<JointMuscleGroupMap>(valueList);
    }

    private JointMuscleGroupMap(JointTypes jointId, MuscleGroupTypes muscleGroupId)
    {
        JointId = jointId;
        MuscleGroupId = muscleGroupId;
    }

    public static readonly IReadOnlyList<JointMuscleGroupMap> Values;

    internal static readonly IReadOnlyDictionary<MuscleGroupTypes, IReadOnlyCollection<Joint>> MuscleGroupMap;

    internal static readonly IReadOnlyDictionary<JointTypes, IReadOnlyCollection<MuscleGroup>> JointMap;

    private static IList<JointMuscleGroupMap> ValueList()
    {
        return new List<JointMuscleGroupMap>
        {
            new (JointTypes.Neck, MuscleGroupTypes.Neck),
            new (JointTypes.Shoulder, MuscleGroupTypes.Neck),
            new (JointTypes.UpperBack, MuscleGroupTypes.Neck),
            new (JointTypes.Neck, MuscleGroupTypes.Shoulder),
            new (JointTypes.Shoulder, MuscleGroupTypes.Shoulder),
            new (JointTypes.UpperBack, MuscleGroupTypes.Shoulder),
            new (JointTypes.Shoulder, MuscleGroupTypes.Chest),
            new (JointTypes.Shoulder, MuscleGroupTypes.UpperBack),
            new (JointTypes.UpperBack, MuscleGroupTypes.UpperBack),
            new (JointTypes.Elbow, MuscleGroupTypes.UpperArm),
            new (JointTypes.Shoulder, MuscleGroupTypes.UpperArm),
            new (JointTypes.Elbow, MuscleGroupTypes.Forearm),
            new (JointTypes.Wrist, MuscleGroupTypes.Forearm),
            new (JointTypes.UpperBack, MuscleGroupTypes.LowerBack),
            new (JointTypes.LowerBack, MuscleGroupTypes.LowerBack),
            new (JointTypes.LowerBack, MuscleGroupTypes.Abdomen),
            new (JointTypes.Hip, MuscleGroupTypes.Hip),
            new (JointTypes.Hip, MuscleGroupTypes.Thigh),
            new (JointTypes.Knee, MuscleGroupTypes.Thigh),
            new (JointTypes.Knee, MuscleGroupTypes.Calve),
            new (JointTypes.Ankle, MuscleGroupTypes.Calve)
        };
    }
}