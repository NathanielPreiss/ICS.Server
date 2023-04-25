namespace ICS.Muscle;

public class Joint
{
    public JointTypes JointId { get; }
    public string JointName { get; }

    public IReadOnlyCollection<MuscleGroup> MuscleGroups => JointMuscleGroupMap.JointMap[JointId];

    public static IReadOnlyDictionary<JointTypes, Joint> Lookup { get; }
    public static IReadOnlyList<Joint> Values { get; }

    static Joint()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<JointTypes, Joint>(valueList.ToDictionary(value => value.JointId));
        Values = new ReadOnlyCollection<Joint>(valueList);
    }

    [JsonConstructor]
    private Joint()
    {
        JointId = JointTypes.Invalid;
        JointName = string.Empty;
    }

    private Joint(JointTypes jointId, string jointName)
    {
        JointId = jointId;
        JointName = jointName;
    }

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