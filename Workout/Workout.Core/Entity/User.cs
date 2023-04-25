namespace ICS.Workout;

public class User
{
    public Guid UserId { get; set; }

    public ICollection<Workout>? Workouts { get; set; }

    [JsonConstructor]
    private User()
        : this(Guid.Empty)
    {
    }

    public User(Guid userId)
    {
        UserId = userId;

        Workouts = null;
    }
}
