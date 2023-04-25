namespace ICS.Workout;

public class Routine
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public int Position { get; set; }
    public ExerciseTypes ExerciseId { get; set; }

    public Workout? Workout { get; set; }
    public ICollection<Set>? Sets { get; set; }

    [JsonConstructor]
    private Routine()
        : this(Guid.Empty, Guid.Empty, 0, ExerciseTypes.Invalid)
    {
    }

    public Routine(Guid workoutId, Guid routineId, int position, ExerciseTypes exerciseId)
    {
        WorkoutId = workoutId;
        RoutineId = routineId;
        Position = position;
        ExerciseId = exerciseId;

        Workout = null;
        Sets = null;
    }
}
