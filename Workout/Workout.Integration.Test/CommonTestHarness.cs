using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ICS.Workout.Test;

public static class CommonTestHarness
{
    public static void AssemblyInitialize(TestContext _)
    {
        Console.WriteLine(@"Common AssemblyInitialize");

        Console.WriteLine(@"Defaulting Faker to Strict");
        Faker.DefaultStrictMode = true;
    }

    public static IContainer DefaultContainer(string applicationName, string configFileName, string connectionStringName) 
    {
        var builder = new ContainerBuilder();

        Host.CreateDefaultBuilder()
            .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.SetMinimumLevel(LogLevel.Warning);
                loggingBuilder.AddSimpleConsole();
            })
            .ConfigureServices(services =>
            {
                services.AddDbContextFactory<WorkoutDbContext>();
                builder.Populate(services);
            }).Build().Dispose();

        var config = GetConfiguration(configFileName);
        
        builder.RegisterModule(new WorkoutModule(applicationName, false, config.GetConnectionString(connectionStringName)));
        
        return builder.Build();
    }

    public static IConfigurationRoot GetConfiguration(string configFileName) =>
        new ConfigurationBuilder().AddJsonFile(configFileName).Build();
}
