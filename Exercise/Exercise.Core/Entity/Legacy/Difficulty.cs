/*namespace ICS.Exercise;

public class Difficulty
{
    public DifficultyTypes DifficultyId { get; }
    public string DifficultyName { get; }

    private Difficulty(DifficultyTypes difficultyId, string difficultyName)
    {
        DifficultyId = difficultyId;
        DifficultyName = difficultyName;
    }

    public static Difficulty Get(DifficultyTypes difficultyId)
    {
        return difficultyId switch
        {
            DifficultyTypes.Beginner => Beginner,
            DifficultyTypes.Experienced => Experienced,
            DifficultyTypes.Advanced => Advanced,
            DifficultyTypes.Invalid or _ => throw new ArgumentOutOfRangeException(nameof(difficultyId), difficultyId, null)
        };
    }

    private static readonly Difficulty Beginner = new(DifficultyTypes.Beginner, "Beginner");
    private static readonly Difficulty Experienced = new(DifficultyTypes.Beginner, "Experienced");
    private static readonly Difficulty Advanced = new(DifficultyTypes.Beginner, "Advanced");
}
*/