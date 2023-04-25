namespace ICS.Exercise;

public static class MechanicResourceMapping
{
    public static string Name(this MechanicTypes mechanicId)
    {
        return Resources.Mechanic_Name.ResourceManager.GetString($"{mechanicId}") ??
               throw new MissingResourceMappingException(nameof(Name), nameof(mechanicId), mechanicId);
    }
}
