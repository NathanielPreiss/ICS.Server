namespace ICS.Api;

public class NServiceBusSendOnlyConfig
{
    public NServiceBusSendOnlyConfig()
    {
        EndpointName = string.Empty;
        License = string.Empty;
    }

    public string EndpointName { get; set; }
    public string License { get; set; }
}
