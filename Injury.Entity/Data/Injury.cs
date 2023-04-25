namespace ICS.Injury;

public class Injury
{
    public int InjuryId { get; set; }
    public int UserId { get; set; }
    public JointTypes? JointId { get; set; }
    public MuscleTypes? MuscleId { get; set; }
    public DateTime InjuryDateUtc { get; set; }

    [JsonConstructor]
    private Injury()
    {
        JointId = null;
        MuscleId = null;
    }

    public Injury(int userId, JointTypes jointId, DateTime injuryDateUtc)
    {
        UserId = userId;
        JointId = jointId;
        InjuryDateUtc = injuryDateUtc;
    }

    public Injury(int userId, MuscleTypes muscleId, DateTime injuryDateUtc)
    {
        UserId = userId;
        MuscleId = muscleId;
        InjuryDateUtc = injuryDateUtc;
    }
}
