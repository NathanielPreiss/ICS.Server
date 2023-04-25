namespace ICS.Muscle;

public class MuscleGroupDto
{
    public MuscleGroupTypes MuscleGroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public BodyAreaTypes BodyAreaId { get; set; }
    public BodyAreaDto? BodyArea { get; set; }

    public IEnumerable<JointTypes> JointIds { get; set; }
    public IEnumerable<JointDto>? Joints { get; set; }

    public IEnumerable<MuscleTypes> MuscleIds { get; set; }
    public IEnumerable<MuscleDto>? Muscles { get; set; }

    private MuscleGroupDto(MuscleGroupTypes muscleGroupId, string name, string description, BodyAreaTypes bodyAreaId)
    {
        MuscleGroupId = muscleGroupId ;
        Name = name;
        Description = description;
        BodyAreaId = bodyAreaId;
        BodyArea = null;
        JointIds = Array.Empty<JointTypes>();
        Joints = null;
        MuscleIds = Array.Empty<MuscleTypes>();
        Muscles = null;
    }

    public MuscleGroupDto(MuscleGroupTypes muscleGroupId, string name, string description, BodyAreaTypes bodyAreaId, 
        IEnumerable<JointTypes> jointIds, IEnumerable<MuscleTypes> muscleIds) 
        : this(muscleGroupId, name, description, bodyAreaId)
    {
        JointIds = jointIds;
        MuscleIds = muscleIds;
    }

    public MuscleGroupDto(MuscleGroupTypes muscleGroupId, string name, string description, BodyAreaDto bodyArea,
        IEnumerable<JointDto> joints, IEnumerable<MuscleDto> muscles)
        : this(muscleGroupId, name, description, bodyArea.BodyAreaId)
    {
        BodyArea = bodyArea;

        Joints = joints.ToList();
        JointIds = Joints.Select(x => x.JointId);

        Muscles = muscles.ToList();
        MuscleIds = Muscles.Select(x => x.MuscleId);
    }
}