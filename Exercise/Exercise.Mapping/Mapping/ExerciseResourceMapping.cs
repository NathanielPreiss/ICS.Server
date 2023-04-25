namespace ICS.Exercise;

public static class ExerciseResourceMapping
{
    public static string Name(this ExerciseTypes exerciseId)
    {
        return Resources.Exercise_Name.ResourceManager.GetString($"{exerciseId}") ??
               throw new MissingResourceMappingException(nameof(Name), nameof(exerciseId), exerciseId);
    }

    public static string Description(this ExerciseTypes exerciseId)
    {
        return Resources.Exercise_Description.ResourceManager.GetString($"{exerciseId}") ??
            throw new MissingResourceMappingException(nameof(Description), nameof(exerciseId), exerciseId);

    }
}
