namespace ICS.Exercise;

public static class MuscleEngagementResourceMapping
{
    public static string Name(this MuscleEngagementTypes muscleEngagementId) =>
        Resources.MuscleEngagement_Name.ResourceManager.GetString($"{muscleEngagementId}") ??
            throw new MissingResourceMappingException(nameof(Name), nameof(muscleEngagementId), muscleEngagementId);
}
