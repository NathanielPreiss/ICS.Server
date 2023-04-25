namespace ICS.Exercise;

public class Utility
{
    public UtilityTypes UtilityId { get; }
    public string UtilityName { get; }

    static Utility()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<UtilityTypes, Utility>(valueList.ToDictionary(value => value.UtilityId));
        Values = new ReadOnlyCollection<Utility>(valueList);
    }

    private Utility(UtilityTypes utilityId, string utilityName)
    {
        UtilityId = utilityId;
        UtilityName = utilityName;
    }

    public static readonly IReadOnlyDictionary<UtilityTypes, Utility> Lookup;
    public static readonly IReadOnlyCollection<Utility> Values;

    private static IList<Utility> ValueList()
    {
        return new List<Utility>
        {
            new(UtilityTypes.Basic, "Basic"),
            new(UtilityTypes.Auxiliary, "Auxiliary"),
            new(UtilityTypes.BasicOrAuxiliary, "Basic or Auxiliary")
        };
    }
}
