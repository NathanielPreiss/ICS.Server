namespace ICS.Exercise;

public class ExerciseDto
{
    public ExerciseTypes ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public string ExerciseDescription { get; set; }

    public ExerciseDto(ExerciseTypes exerciseId, string exerciseName, string exerciseDescription)
    {
        ExerciseId = exerciseId;
        ExerciseName = exerciseName;
        ExerciseDescription = exerciseDescription;
    }
}
