namespace ICS.Workout;

public class Workout
{
    public Guid WorkoutId { get; set; }
    public Guid UserId { get; set; }
    public int Position { get; set; }
    public string Name { get; set; }

    public User? User { get; set; }
    public ICollection<Routine>? Routines { get; set; }

    [JsonConstructor]
    private Workout() 
        : this(default, default, default, string.Empty)
    {
    }

    public Workout(Guid userId, Guid workoutId, int position, string name)
    {
        WorkoutId = workoutId;
        UserId = userId;
        Position = position;
        Name = name;

        User = null;
        Routines = null;
    }
}
