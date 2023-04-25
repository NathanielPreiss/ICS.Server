namespace ICS.Muscle;

public class BodyArea
{
    public BodyAreaTypes BodyAreaId { get; }
    public string BodyAreaName { get; }

    public IReadOnlyCollection<MuscleGroup> MuscleGroups => MuscleGroup.BodyAreaMap[BodyAreaId];

    public static IReadOnlyDictionary<BodyAreaTypes, BodyArea> Lookup { get; }
    public static IReadOnlyCollection<BodyArea> Values { get; }

    static BodyArea()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<BodyAreaTypes, BodyArea>(valueList.ToDictionary(value => value.BodyAreaId));
        Values = new ReadOnlyCollection<BodyArea>(valueList);
    }

    [JsonConstructor]
    private BodyArea()
    {
        BodyAreaId = BodyAreaTypes.Invalid;
        BodyAreaName = string.Empty;
    }

    private BodyArea(BodyAreaTypes bodyAreaId, string bodyAreaName)
    {
        BodyAreaId = bodyAreaId;
        BodyAreaName = bodyAreaName;
    }

    private static IList<BodyArea> ValueList()
    {
        return new List<BodyArea>
        {
            new(BodyAreaTypes.Upper, "Upper Body"),
            new(BodyAreaTypes.Core, "Core"),
            new(BodyAreaTypes.Lower, "Lower Body")
        };
    }
}
