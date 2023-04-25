namespace ICS.Muscle;

public static class MuscleResourceMapping
{
    public static string Name(this MuscleTypes muscleId) =>
        Resources.Muscle_Name.ResourceManager.GetString($"{muscleId}") ??
               throw new MissingResourceMappingException(nameof(Name), nameof(muscleId), muscleId);

    public static string Description(this MuscleTypes muscleId) =>
        Resources.Muscle_Description.ResourceManager.GetString($"{muscleId}") ??
               throw new MissingResourceMappingException(nameof(Description), nameof(muscleId), muscleId);
}
