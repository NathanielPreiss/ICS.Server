namespace ICS.Muscle;

public class JointDto
{
    public JointTypes JointId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public IEnumerable<MuscleGroupTypes> MuscleGroupIds { get; set; }
    public IEnumerable<MuscleGroupDto>? MuscleGroups { get; set; }

    private JointDto(JointTypes jointId, string name, string description)
    {
        JointId = jointId;
        Name = name;
        Description = description;
        MuscleGroupIds = Array.Empty<MuscleGroupTypes>();
        MuscleGroups = null;
    }

    public JointDto(JointTypes jointId, string name, string description, IEnumerable<MuscleGroupTypes> muscleGroupIds)
        : this(jointId, name, description)
    {
        MuscleGroupIds = muscleGroupIds.ToList();
    }

    public JointDto(JointTypes jointId, string name, string description, IEnumerable<MuscleGroupDto> muscleGroups)
        : this(jointId, name, description)
    {
        MuscleGroups = muscleGroups.ToList();
        MuscleGroupIds = MuscleGroups.Select(x => x.MuscleGroupId);
    }
}