namespace ICS.Muscle;

public class MuscleGroup
{
    public MuscleGroupTypes MuscleGroupId { get; }
    public string MuscleGroupName { get; }
    public BodyAreaTypes BodyAreaId { get; }

    public BodyArea BodyArea => BodyArea.Lookup[BodyAreaId];
    public IReadOnlyCollection<Muscle> Muscles => Muscle.MuscleGroupMap[MuscleGroupId];
    public IReadOnlyCollection<Joint> Joints => JointMuscleGroupMap.MuscleGroupMap[MuscleGroupId];

    static MuscleGroup()
    {
        var valueList = ValueList();

        BodyAreaMap = new ReadOnlyDictionary<BodyAreaTypes, IReadOnlyCollection<MuscleGroup>>(valueList
            .GroupBy(x => x.BodyAreaId)
            .ToDictionary(x => x.Key,
                x => (IReadOnlyCollection<MuscleGroup>)new ReadOnlyCollection<MuscleGroup>(x.ToList())));

        Lookup = new ReadOnlyDictionary<MuscleGroupTypes, MuscleGroup>(valueList.ToDictionary(value => value.MuscleGroupId));
        Values = new ReadOnlyCollection<MuscleGroup>(valueList);
    }

    private MuscleGroup(MuscleGroupTypes muscleGroupId, string muscleGroupName, BodyAreaTypes bodyAreaId)
    {
        MuscleGroupId = muscleGroupId;
        MuscleGroupName = muscleGroupName;
        BodyAreaId = bodyAreaId;
    }

    public static readonly IReadOnlyDictionary<MuscleGroupTypes, MuscleGroup> Lookup;

    public static readonly IReadOnlyCollection<MuscleGroup> Values;

    internal static readonly IReadOnlyDictionary<BodyAreaTypes, IReadOnlyCollection<MuscleGroup>> BodyAreaMap;

    private static IList<MuscleGroup> ValueList()
    {
        return new List<MuscleGroup>
        {
            new(MuscleGroupTypes.Neck, "Neck", BodyAreaTypes.Upper),
            new(MuscleGroupTypes.Shoulder, "Shoulder", BodyAreaTypes.Upper),
            new(MuscleGroupTypes.Chest, "Chest", BodyAreaTypes.Upper),
            new(MuscleGroupTypes.UpperBack, "Upper Back", BodyAreaTypes.Upper),
            new(MuscleGroupTypes.UpperArm, "Upper Arm", BodyAreaTypes.Upper),
            new(MuscleGroupTypes.Forearm, "Forearm", BodyAreaTypes.Upper),
            new(MuscleGroupTypes.LowerBack, "Lower Back", BodyAreaTypes.Core),
            new(MuscleGroupTypes.Abdomen, "Abdomen", BodyAreaTypes.Core),
            new(MuscleGroupTypes.Hip, "Hip", BodyAreaTypes.Lower),
            new(MuscleGroupTypes.Thigh, "Thigh", BodyAreaTypes.Lower),
            new(MuscleGroupTypes.Calve, "Calve", BodyAreaTypes.Lower)
        };
    }
}
