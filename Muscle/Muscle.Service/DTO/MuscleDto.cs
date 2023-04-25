namespace ICS.Muscle;

public class MuscleDto
{
    public MuscleTypes MuscleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public MuscleGroupTypes MuscleGroupId { get; set; }
    public MuscleGroupDto? MuscleGroup { get; set; }

    public MuscleDto(MuscleTypes muscleId, string name, string description, MuscleGroupTypes muscleGroupId)
    {
        MuscleId = muscleId;
        Name = name;
        Description = description;
        MuscleGroupId = muscleGroupId;
        MuscleGroup = null;
    }

    public MuscleDto(MuscleTypes muscleId, string name, string description, MuscleGroupDto muscleGroup)
        : this(muscleId, name, description, muscleGroup.MuscleGroupId)
    {
        MuscleGroup = muscleGroup;
    }
}