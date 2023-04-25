namespace ICS.Muscle;

public static class BodyAreaResourceMapping
{
    public static string Name(this BodyAreaTypes bodyAreaId) =>
        Resources.BodyArea_Name.ResourceManager.GetString($"{bodyAreaId}") ??
               throw new MissingResourceMappingException(nameof(Name), nameof(bodyAreaId), bodyAreaId);

    public static string Description(this BodyAreaTypes bodyAreaId) =>
        Resources.BodyArea_Description.ResourceManager.GetString($"{bodyAreaId}") ??
               throw new MissingResourceMappingException(nameof(Description), nameof(bodyAreaId), bodyAreaId);
}
