namespace ICS.Exercise;

public class Mechanic
{
    public MechanicTypes MechanicId { get; }
    public string MechanicName { get; }

    static Mechanic()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<MechanicTypes, Mechanic>(valueList.ToDictionary(value => value.MechanicId));
        Values = new ReadOnlyCollection<Mechanic>(valueList);
    }

    private Mechanic(MechanicTypes mechanicId, string mechanicName)
    {
        MechanicId = mechanicId;
        MechanicName = mechanicName;
    }

    public static readonly IReadOnlyDictionary<MechanicTypes, Mechanic> Lookup;
    public static readonly IReadOnlyCollection<Mechanic> Values;

    private static IList<Mechanic> ValueList()
    {
        return new List<Mechanic>
        {
            new(MechanicTypes.Compound, "Compound"),
            new(MechanicTypes.Isolated, "Isolated")
        };
    }
}
