/*namespace ICS.Exercise;

public class Classification
{
    public ExerciseTypes ExerciseId { get; set; }
    public ForceTypes ForceId { get; set; }
    public MechanicTypes MechanicId { get; set; }
    public UtilityTypes UtilityId { get; set; }

    public Exercise Exercise { get; set; }
    public Force Force { get; set; }
    public Mechanic? Mechanic { get; set; }
    public Utility? Utility { get; set; }

    public Classification(ExerciseTypes exerciseId, ForceTypes forceId, MechanicTypes mechanicId, UtilityTypes utilityId)
    {
        ExerciseId = exerciseId;
        ForceId = forceId;
        MechanicId = mechanicId;
        UtilityId = utilityId;

        Exercise = null;
        Force = null;
        Mechanic = null;
        Utility = null;
    }

    private Classification(Exercise exercise, Force force, Mechanic mechanic, Utility utility)
    {
        Exercise = exercise;
        Force = force;
        Mechanic = mechanic;
        Utility = utility;
    }

    public static readonly Classification ChestPress = 
        new(Exercise.ChestPress, Force.Push, Mechanic.Compound, Utility.Basic);
}*/
