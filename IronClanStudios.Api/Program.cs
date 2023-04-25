using System.Reflection;

namespace ICS.Api;

internal static class Program
{
    private static void Main(string[] args)
    {
        var host = HostBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(ConfigureContainer)
            .ConfigureWebHostDefaults(ConfigureWebHostDefaults)
            .ConfigureServices(ConfigureServices)
            .Build();

        host.Run();
    }

    private static IHostBuilder HostBuilder(string[] args)
    {
        // Create the host builder to start the app
        var builder = Host.CreateDefaultBuilder(args);

        // Setup application logging settings
        builder.ConfigureLogging((hostBuilderContext, loggingBuilder) =>
        {
            var loggingLevel = hostBuilderContext.HostingEnvironment.IsDevelopment() ?
                LogLevel.Information : LogLevel.Warning;

            loggingBuilder.SetMinimumLevel(loggingLevel);

            loggingBuilder.AddFilter("Microsoft", LogLevel.Information);

            if (!hostBuilderContext.HostingEnvironment.IsProduction())
            {
                loggingBuilder.AddJsonConsole(options =>
                {
                    options.JsonWriterOptions = new JsonWriterOptions {Indented = true};
                    //options.IncludeScopes = true;
                });
                loggingBuilder.AddDebug();
            }

            loggingBuilder.AddApplicationInsights();
                //.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, loggingLevel);
        });

        // Setup NServiceBus usage
        builder.UseNServiceBus(hostContext =>
        {
            var nServiceBusConfig = hostContext.Configuration.GetSection("NServiceBus").Get<NServiceBusSendOnlyConfig>();

            string hostName, instanceName;
            RoutingSettings routing;

            var endpointConfiguration = new EndpointConfiguration(nServiceBusConfig.EndpointName);

            if (hostContext.HostingEnvironment.IsDevelopment())
            {
                hostName = "LOCAL_HOST";
                instanceName = "LOCAL";

                routing = endpointConfiguration.SetupLearningTransport(TransportTransactionMode.ReceiveOnly).Routing();
            }
            else if (hostContext.HostingEnvironment.IsStaging() || hostContext.HostingEnvironment.IsProduction())
            {
                hostName = hostContext.Configuration.GetValue("WEBSITE_SITE_NAME", "UNKNOWN_HOST");
                instanceName = hostContext.Configuration.GetValue("WEBSITE_INSTANCE_ID", "UNKNOWN_INSTANCE");

                var nsbTransportConnectionString = hostContext.Configuration.GetConnectionString("NServiceBusTransport");
                routing = endpointConfiguration.SetupAzureTransport(nsbTransportConnectionString, TransportTransactionMode.ReceiveOnly).Routing();
            }
            else
            {
                throw new Exception();
            }

            endpointConfiguration.SetupDefaults(nServiceBusConfig.License, hostName, instanceName);

            return endpointConfiguration;
        });

        return builder;
    }

    private static void ConfigureContainer(HostBuilderContext hostContext, ContainerBuilder builder)
    {
        var auth0ConnectionString = hostContext.Configuration.GetConnectionString("Auth0Database");
        builder.RegisterModule(new Auth0Module(hostContext.HostingEnvironment.ApplicationName, hostContext.HostingEnvironment.IsProduction(), auth0ConnectionString));

        var exerciseConnectionString = hostContext.Configuration.GetConnectionString("ExerciseDatabase");
        builder.RegisterModule(new ExerciseModule(hostContext.HostingEnvironment.ApplicationName, hostContext.HostingEnvironment.IsProduction(), exerciseConnectionString));

        var equipmentConnectionString = hostContext.Configuration.GetConnectionString("EquipmentDatabase");
        builder.RegisterModule(new EquipmentModule(hostContext.HostingEnvironment.ApplicationName, hostContext.HostingEnvironment.IsProduction(), equipmentConnectionString));

        var muscleConnectionString = hostContext.Configuration.GetConnectionString("MuscleDatabase");
        builder.RegisterModule(new MuscleModule(hostContext.HostingEnvironment.ApplicationName, hostContext.HostingEnvironment.IsProduction(), muscleConnectionString));

        var workoutConnectionString = hostContext.Configuration.GetConnectionString("WorkoutDatabase");
        builder.RegisterModule(new WorkoutModule(hostContext.HostingEnvironment.ApplicationName, hostContext.HostingEnvironment.IsProduction(), workoutConnectionString));

#if DEBUG  // This is so any locally created migrations get applied on startup
        if (hostContext.HostingEnvironment.IsDevelopment())
        {
            builder.RegisterBuildCallback(scope =>
            {
                scope.Resolve<IDbContextFactory<Auth0DbContext>>().CreateDbContext().Database.Migrate();
                scope.Resolve<IDbContextFactory<EquipmentDbContext>>().CreateDbContext().Database.Migrate();
                scope.Resolve<IDbContextFactory<ExerciseDbContext>>().CreateDbContext().Database.Migrate();
                scope.Resolve<IDbContextFactory<MuscleDbContext>>().CreateDbContext().Database.Migrate();
                scope.Resolve<IDbContextFactory<WorkoutDbContext>>().CreateDbContext().Database.Migrate();
            });
        }
#endif
    }

    private static void ConfigureWebHostDefaults(IWebHostBuilder webBuilder)
    {
        webBuilder.Configure((context, builder) =>
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }

            builder.UseResponseCompression();
            builder.UseResponseCaching();

            if (context.HostingEnvironment.IsProduction())
            {
                builder.UseHttpsRedirection();
            }

            builder.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            builder.UseRouting();
            builder.UseAuthentication();
            builder.UseAuthorization();

            builder.UseRequestLocalization();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });

            builder.UseSwagger(options => options.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                swagger.Servers.Add(new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" });
            }));

            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Iron Clan Studios API");

                c.DisplayOperationId();
                c.DisplayRequestDuration();

                c.ConfigObject.DefaultModelsExpandDepth = 0;
                c.ConfigObject.TryItOutEnabled = true;
                c.ConfigObject.PersistAuthorization = true;

                var authConfig = context.Configuration.GetSection(AuthenticationConfiguration.Section)
                    .Get<AuthenticationConfiguration>();

                c.OAuthClientId(authConfig.ClientId);
                c.OAuthClientSecret(authConfig.ClientSecret);

                // This is because we can't access the request body
                c.UseRequestInterceptor("(req) => { if (req.url.endsWith('oauth/token') && req.body) req.body += '&audience=" + authConfig.Audience + "'; return req; }");
            });
        });
    }

    private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressInferBindingSourcesForParameters = true;
        });

        var authConfig = hostContext.Configuration.GetSection(AuthenticationConfiguration.Section)
            .Get<AuthenticationConfiguration>();

        services.AddRequestLocalization(options =>
        {
            options
                .SetDefaultCulture(Constants.DefaultCulture)
                .AddSupportedCultures(Constants.SupportedCultures)
                .AddSupportedUICultures(Constants.SupportedCultures);
            options.ApplyCurrentCultureToResponseHeaders = true;
        });

        // Handles remainder of jwt validation
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidAudience = authConfig.Audience,
                ValidIssuer = authConfig.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.SigningKey))
            };
        });

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            WorkoutModule.ApplyPolicies(options.AddPolicy);
        }).AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();

        services.AddResponseCaching();
        services.AddResponseCompression();

        var serviceConfig = hostContext.Configuration.Get<ServiceConfiguration>();

        services.AddApplicationInsightsTelemetry(options =>
        {
            options.ConnectionString = hostContext.Configuration.GetConnectionString("ApplicationInsights");
        }).ConfigureTelemetryModule((DependencyTrackingTelemetryModule module, ApplicationInsightsServiceOptions _) =>
        {
            module.EnableSqlCommandTextInstrumentation = serviceConfig.EnableSqlCommandInstrumentation;
        });

        // Database context factories
        services.AddPooledDbContextFactory<Auth0DbContext>(_ => { });
        services.AddPooledDbContextFactory<EquipmentDbContext>(_ => { });
        services.AddPooledDbContextFactory<ExerciseDbContext>(_ => { });
        services.AddPooledDbContextFactory<MuscleDbContext>(_ => { });
        services.AddPooledDbContextFactory<WorkoutDbContext>(_ => { });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Iron Clan Studios API",
                    Version = "v1",
                    Description = "hello, this is a description",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Nathaniel Preiss",
                        Email = "nathaniel.preiss@gmail.com",
                        Url = new Uri("https://twitter.com/npreiss")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "ICS API",
                        Url = new Uri("https://example.com/license")
                    }
                });

            var scheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Description",
                Name = "Authorization",
                Type = SecuritySchemeType.OAuth2,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{authConfig.Issuer}authorize?audience={authConfig.Audience}")
                    },
                    Password = new OpenApiOAuthFlow
                    {
                        // Audience must be added to the body for Auth0
                        TokenUrl = new Uri($"{authConfig.Issuer}oauth/token")
                    }
                }
            };

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, scheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {{ scheme, new List<string>() }});

            c.EnableAnnotations();
        });

        services.AddSwaggerDocument();
    }
}
