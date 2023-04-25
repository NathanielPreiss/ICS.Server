namespace ICS.Workout;

[SwaggerSchema("Set response body.")]
public class SetResponse
{
    [SwaggerSchema("Workout identifier.")]
    public Guid WorkoutId { get; set; }
    
    [SwaggerSchema("The routine identifier.")] 
    public int RoutineId { get; set; }
    
    [SwaggerSchema("Set identifier.")]
    public int SetId { get; set; }
    
    [SwaggerSchema("The number of repetitions in the set.")]
    public int Reps { get; set; }
    
    [SwaggerSchema("The resistance weight.")]
    public int Weight { get; set; }
}
