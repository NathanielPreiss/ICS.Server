namespace ICS.Workout;

[SwaggerSchema("Workout request body.")]
public class WorkoutRequest
{
    [Required, SwaggerSchema("Workout identifier.")]
    public Guid WorkoutId { get; set; }
    [Required, SwaggerSchema("Workout's name.")]
    public string Name { get; set; }
    public int Position { get; set; }

    [Required, SwaggerSchema("The workout's list of routines.")]
    public IEnumerable<RoutineRequest> Routines { get; set; }
}

[SwaggerSchema("Routine request body.")]
public class RoutineRequest
{
    //[Required, SwaggerSchema("Routine identifier.")]
    //public int RoutineId { get; set; }
    [Required, SwaggerSchema("The routine's exercise.")]
    public ExerciseTypes ExerciseId { get; set; }

    public int Position { get; set; }

    [Required, SwaggerSchema("The routine's list of sets.")]
    public ICollection<SetRequest>? Sets { get; set; }
}

[SwaggerSchema("Set request body.")]
public class SetRequest
{
    //[FromRoute, SwaggerParameter("The workout identifier.", Required = true), SwaggerSchema("Set identifier.")]
    //public Guid workoutId { get; set; }
    //
    //[FromRoute, SwaggerParameter("The routine identifier.", Required = true)] 
    //public int routineId { get; set; }
    //
    //[Required, SwaggerSchema("Set identifier.")]
    //public int SetId { get; set; }

    [Required, SwaggerSchema("The number of repetitions in the set.")]
    public int Reps { get; set; }
    [Required, SwaggerSchema("The resistance weight.")]
    public int Weight { get; set; }
}


public class PatchWorkoutRequest
{
    public string Name { get; set; }
}

public class PostWorkoutRequest
{
    public string Name { get; set; }
    public ICollection<PostRoutineRequest>? Routines { get; set; }
}

public class PatchRoutineRequest
{
    public ExerciseTypes ExerciseId { get; set; }
}

public class PostRoutineRequest
{
    public ExerciseTypes ExerciseId { get; set; }
    public ICollection<PostSetRequest>? Sets { get; set; }
}

public class PatchSetRequest
{
    public int Reps { get; set; }
    public int Weight { get; set; }
}

public class PostSetRequest
{
    public int Reps { get; set; }
    public int Weight { get; set; }
}
