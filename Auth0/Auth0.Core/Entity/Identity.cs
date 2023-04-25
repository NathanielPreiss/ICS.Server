namespace ICS.Auth0;

public class Identity
{
    public Identity(Guid userId, string provider, string providerId, DateTime createdUtc)
    {
        UserId = userId;
        Provider = provider;
        ProviderId = providerId;
        CreatedUtc = createdUtc;
    }

    public Identity(Guid userId, string providerIdentity, DateTime createdUtc)
    {
        UserId = userId;
        var splitProvider = providerIdentity.Split('|', 2);
        if (splitProvider.Length != 2)
        {
            throw new ArgumentException($"Provider Identification value {providerIdentity} was invalid", nameof(providerIdentity));
        }
        Provider = splitProvider.First();
        ProviderId = splitProvider.Last();
        CreatedUtc = createdUtc;
    }

    [JsonConstructor]
    private Identity()
        : this(Guid.Empty, string.Empty, string.Empty, DateTime.MinValue)
    {
    }

    public Guid UserId { get; set; }
    public string Provider { get; set; }
    public string ProviderId { get; set; }
    public DateTime CreatedUtc { get; set; }
    public string ProviderIdentity => $"{Provider}|{ProviderId}";

    public User? User { get; set; }
}
