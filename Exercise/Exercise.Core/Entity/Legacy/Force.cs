/*namespace ICS.Exercise;

public class Force
{
    public ForceTypes ForceId { get; }
    public string ForceName { get; }

    private Force(ForceTypes forceId, string forceName)
    {
        ForceId = forceId;
        ForceName = forceName;
    }

    public static Force Get(ForceTypes forceId)
    {
        return forceId switch
        {
            ForceTypes.Push => Push,
            ForceTypes.Pull => Pull,
            ForceTypes.Invalid or _ => throw new ArgumentOutOfRangeException(nameof(forceId), forceId, null)
        };
    }

    public static readonly Force Push = new(ForceTypes.Push, "Push");
    public static readonly Force Pull = new(ForceTypes.Pull, "Pull");
}
*/