namespace ICS.Muscle;

public static class MuscleGroupResourceMapping
{
    public static string Name(this MuscleGroupTypes muscleGroupId) =>
        Resources.MuscleGroup_Name.ResourceManager.GetString($"{muscleGroupId}") ??
            throw new MissingResourceMappingException(nameof(Name), nameof(muscleGroupId), muscleGroupId);

    public static string Description(this MuscleGroupTypes muscleGroupId) =>
        Resources.MuscleGroup_Description.ResourceManager.GetString($"{muscleGroupId}") ??
               throw new MissingResourceMappingException(nameof(Description), nameof(muscleGroupId), muscleGroupId);
}
