namespace ICS.Muscle;

public class BodyAreaDto
{
    public BodyAreaTypes BodyAreaId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public IEnumerable<MuscleGroupTypes> MuscleGroupIds { get; set; }
    public IEnumerable<MuscleGroupDto>? MuscleGroups { get; set; }

    private BodyAreaDto(BodyAreaTypes bodyAreaId, string name, string description)
    {
        BodyAreaId = bodyAreaId;
        Name = name;
        Description = description;
        MuscleGroupIds = Array.Empty<MuscleGroupTypes>();
        MuscleGroups = null;
    }

    public BodyAreaDto(BodyAreaTypes bodyAreaId, string name, string description, IEnumerable<MuscleGroupTypes> muscleGroupIds)
        : this(bodyAreaId, name, description)
    {
        MuscleGroupIds = muscleGroupIds.ToList();
    }

    public BodyAreaDto(BodyAreaTypes bodyAreaId, string name, string description, IEnumerable<MuscleGroupDto> muscleGroups) 
        : this(bodyAreaId, name, description)
    {
        MuscleGroups = muscleGroups.ToList();
        MuscleGroupIds = MuscleGroups.Select(x => x.MuscleGroupId);
    }
}