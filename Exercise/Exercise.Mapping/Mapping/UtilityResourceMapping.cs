namespace ICS.Exercise;

public static class MuscleGroupResourceMapping
{
    public static string Name(this UtilityTypes utilityId) =>
        Resources.Utility_Name.ResourceManager.GetString($"{utilityId}") ??
            throw new MissingResourceMappingException(nameof(Name), nameof(utilityId), utilityId);
}
