using NServiceBus.Logging;

namespace ICS.Api;

public static class EndpointConfigurationExtensions
{
    private const string AuditQueue = "audit";
    private const string ErrorQueue = "error";

    public static void SetupDefaults(
        this EndpointConfiguration configuration,
        string license,
        string hostName,
        string instanceName)
    {
        LogManager.Use<DefaultFactory>().Level(NServiceBus.Logging.LogLevel.Info);

        configuration.License(license);

        configuration.UseSerialization<NewtonsoftJsonSerializer>();

        configuration.EnableInstallers();

        var displayName = $"{hostName}_{instanceName[..5]}";

        configuration
            .UniquelyIdentifyRunningInstance()
            .UsingNames(instanceName, hostName)
            .UsingCustomDisplayName(displayName);

        configuration.DefineCriticalErrorAction(OnCriticalError);
    }

    public static TransportExtensions<LearningTransport> SetupLearningTransport(
        this EndpointConfiguration configuration,
        TransportTransactionMode transactionMode)
    {
        var transport = configuration.UseTransport<LearningTransport>();
        transport.Transactions(transactionMode);

        return transport;
    }

    public static TransportExtensions<AzureServiceBusTransport> SetupAzureTransport(
        this EndpointConfiguration configuration,
        string connectionString,
        TransportTransactionMode transactionMode)
    {
        var transport = configuration.UseTransport<AzureServiceBusTransport>();
        transport.Transactions(transactionMode);
        transport.ConnectionString(connectionString);
        transport.SubscriptionRuleNamingConvention(Sha1Sanitization.SanitizeType);
        transport.SubscriptionNamingConvention(Sha1Sanitization.SanitizeName);

        return transport;
    }

    public static void SetupAuditMetrics(
        this EndpointConfiguration configuration,
        bool enableAudit,
        bool enableHeartbeat,
        bool enableMetrics,
        string serviceControl,
        string metricsServiceControl)
    {
        configuration.SendFailedMessagesTo(ErrorQueue);

        if (enableAudit)
            configuration.AuditProcessedMessagesTo(AuditQueue);

        const int pulseFrequency = 2;
        if (enableHeartbeat)
            configuration.SendHeartbeatTo(serviceControl, TimeSpan.FromSeconds(pulseFrequency));

        if (enableMetrics)
            configuration.EnableMetrics().SendMetricDataToServiceControl(metricsServiceControl, TimeSpan.FromSeconds(pulseFrequency));
    }

    private static async Task OnCriticalError(
        ICriticalErrorContext context)
    {
        var fatalMessage = $@"The following critical error was encountered:{Environment.NewLine}{context.Error}{Environment.NewLine}Process is shutting down. 
                               StackTrace: {Environment.NewLine}{context.Exception.StackTrace}";

        try
        {
            await context.Stop().ConfigureAwait(false);
        }
        finally
        {
            Environment.FailFast(fatalMessage, context.Exception);
        }
    }
}
