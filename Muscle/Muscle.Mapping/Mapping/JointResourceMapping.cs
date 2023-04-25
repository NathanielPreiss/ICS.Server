namespace ICS.Muscle;

public static class JointResourceMapping
{
    public static string Name(this JointTypes jointId) =>
        Resources.Joint_Name.ResourceManager.GetString($"{jointId}") ??
               throw new MissingResourceMappingException(nameof(Name), nameof(jointId), jointId);

    public static string Description(this JointTypes jointId) =>
        Resources.Joint_Description.ResourceManager.GetString($"{jointId}") ??
               throw new MissingResourceMappingException(nameof(Description), nameof(jointId), jointId);
}
