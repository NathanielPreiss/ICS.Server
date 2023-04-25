namespace ICS.Exercise;

public class Classification
{
    public ExerciseTypes ExerciseId { get; }
    public MechanicTypes MechanicId { get; }
    public UtilityTypes UtilityId { get; }

    public Exercise Exercise => Exercise.Lookup[ExerciseId];
    public Utility Utility => Utility.Lookup[UtilityId];
    public Mechanic Mechanic => Mechanic.Lookup[MechanicId];

    public static IReadOnlyDictionary<ExerciseTypes, Classification> Lookup { get; }
    public static IReadOnlyCollection<Classification> Values { get; }

    static Classification()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<ExerciseTypes, Classification>(valueList.ToDictionary(value => value.ExerciseId));
        Values = new ReadOnlyCollection<Classification>(valueList);
    }

    private Classification(ExerciseTypes exerciseId, MechanicTypes mechanicId, UtilityTypes utilityId)
    {
        ExerciseId = exerciseId;
        MechanicId = mechanicId;
        UtilityId = utilityId;
    }

    private static IList<Classification> ValueList()
    {
        return new List<Classification>
        {
            new(ExerciseTypes.NeckFlexion, MechanicTypes.Isolated, UtilityTypes.BasicOrAuxiliary),
            new(ExerciseTypes.NeckExtension, MechanicTypes.Isolated, UtilityTypes.BasicOrAuxiliary),
            new(ExerciseTypes.FrontRaise, MechanicTypes.Isolated, UtilityTypes.Auxiliary),
            new(ExerciseTypes.UprightRow, MechanicTypes.Compound, UtilityTypes.Basic),
            new(ExerciseTypes.RearRow, MechanicTypes.Compound, UtilityTypes.BasicOrAuxiliary),
            new(ExerciseTypes.FrontLateralRaise, MechanicTypes.Isolated, UtilityTypes.Auxiliary),
            new(ExerciseTypes.ChestPress, MechanicTypes.Compound, UtilityTypes.Basic)
        };
    }
}
