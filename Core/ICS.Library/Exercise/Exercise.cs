namespace ICS.Exercise;

public class Exercise
{
    public ExerciseTypes ExerciseId { get; }
    public string ExerciseName { get; }

    public Classification Classification => Classification.Lookup[ExerciseId];
    public ExerciseEngagementMuscleMap MuscleMap => ExerciseEngagementMuscleMap.Lookup[ExerciseId];

    static Exercise()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<ExerciseTypes, Exercise>(valueList.ToDictionary(value => value.ExerciseId));
        Values = new ReadOnlyCollection<Exercise>(valueList);
    }

    private Exercise(ExerciseTypes exerciseId, string exerciseName)
    {
        ExerciseId = exerciseId;
        ExerciseName = exerciseName;
    }

    public static readonly IReadOnlyDictionary<ExerciseTypes, Exercise> Lookup;
    public static readonly IReadOnlyCollection<Exercise> Values;

    private static IList<Exercise> ValueList()
    {
        return new List<Exercise>
        {
            new(ExerciseTypes.NeckFlexion, "Neck Flexion"),
            new(ExerciseTypes.NeckExtension, "Neck Extension"),
            new(ExerciseTypes.FrontRaise, "Front Raise"),
            new(ExerciseTypes.UprightRow, "Upright Row"),
            new(ExerciseTypes.RearRow, "Rear Row")
        };
    }
}
