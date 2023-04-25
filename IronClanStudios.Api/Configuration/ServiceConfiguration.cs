namespace ICS.Api;

public class ServiceConfiguration
{
    public bool EnableSqlCommandInstrumentation { get; set; }
    public string InstrumentationKey { get; set; }

    public ServiceConfiguration()
    {
        EnableSqlCommandInstrumentation = true;
        InstrumentationKey = string.Empty;
    }
}
