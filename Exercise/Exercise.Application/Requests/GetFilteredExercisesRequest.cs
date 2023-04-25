namespace ICS.Exercise;

[SwaggerSchema("A list of parameters to filter out exercises.")]
public class GetFilteredExercisesRequest
{
    [Required, SwaggerSchema("The target body area for the exercises.")]
    public BodyAreaTypes BodyAreaId { get; set; }

    [SwaggerSchema("The number of exercises to return.")]
    public int ExerciseCount { get; set; } = 10;

    [SwaggerSchema("A list of injured joints to avoid.")]
    public IEnumerable<JointTypes> ExcludeJoints { get; set; }
    
    [SwaggerSchema("A list of injured muscles to avoid.")]
    public IEnumerable<MuscleTypes> ExcludeMuscles { get; set; }
    
    [SwaggerSchema("A list of equipment available.")]
    public IEnumerable<EquipmentGroupTypes> IncludeEquipmentGroups { get; set; }
}
