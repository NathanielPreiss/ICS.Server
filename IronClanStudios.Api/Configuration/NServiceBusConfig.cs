namespace ICS.Api;

public class NServiceBusConfig : NServiceBusSendOnlyConfig
{
    public NServiceBusConfig()
    {
        ServiceControl = string.Empty;
        MetricsServiceControl = string.Empty;
    }

    public bool EnableAudit { get; set; }
    public bool EnableHeartbeat { get; set; }
    public bool EnableMetrics { get; set; }
    public string ServiceControl { get; set; }
    public string MetricsServiceControl { get; set; }
    public int DelayedRetries { get; set; }
    public int ImmediateRetries { get; set; }
}
