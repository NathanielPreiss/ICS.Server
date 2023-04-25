namespace ICS.Workout;

public class Set
{
    public Guid RoutineId { get; set; }
    public Guid SetId { get; set; }
    public int Position { get; set; }
    public int Reps { get; set; }
    public int Weight { get; set; }

    public Routine? Routine { get; set; }

    [JsonConstructor]
    private Set()
        : this(default, default, default, default, default)
    {
    }

    public Set(Guid routineId, Guid setId, int position, int reps, int weight)
    {
        RoutineId = routineId;
        SetId = setId;
        Position = position;
        Reps = reps;
        Weight = weight;

        Routine = null;
    }
}
