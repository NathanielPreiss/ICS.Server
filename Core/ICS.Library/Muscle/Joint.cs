namespace ICS.Muscle;

public class Joint
{
    public JointTypes JointId { get; }
    public string JointName { get; }

    public IReadOnlyCollection<MuscleGroup> MuscleGroups => JointMuscleGroupMap.JointMap[JointId];

    static Joint()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<JointTypes, Joint>(valueList.ToDictionary(value => value.JointId));
        Values = new ReadOnlyCollection<Joint>(valueList);
    }

    private Joint(JointTypes jointId, string jointName)
    {
        JointId = jointId;
        JointName = jointName;
    }

    public static readonly IReadOnlyDictionary<JointTypes, Joint> Lookup;
    public static readonly IReadOnlyList<Joint> Values;

    private static IList<Joint> ValueList()
    {
        return new List<Joint>
        {
            new (JointTypes.Neck, "Neck"),
            new (JointTypes.Shoulder, "Shoulder"),
            new (JointTypes.Elbow, "Elbow"),
            new (JointTypes.Wrist, "Wrist"),
            new (JointTypes.UpperBack, "Upper Back"),
            new (JointTypes.LowerBack, "Lower Back"),
            new (JointTypes.Hip, "Hip"),
            new (JointTypes.Knee, "Knee"),
            new (JointTypes.Ankle, "Ankle")
        };
    }
}
