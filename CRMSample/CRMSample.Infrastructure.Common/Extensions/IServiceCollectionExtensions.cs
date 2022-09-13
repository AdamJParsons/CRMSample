using CRMSample.Infrastructure.Common.Middleware.HealthChecks;
using CRMSample.Infrastructure.Common.Settings;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration, Action<AuthenticationOptions> authenticationConfig = null, Action<JwtBearerOptions> jwtConfig = null)
        {
            AuthenticationSettings authenticationSettings = new AuthenticationSettings();
            configuration.Bind(nameof(AuthenticationSettings), authenticationSettings);
            services.AddSingleton<AuthenticationSettings>(authenticationSettings);

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authenticationConfig?.Invoke(config);
            })
            .AddJwtBearer(config =>
            {
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationSettings.AuthenticationKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
                jwtConfig?.Invoke(config);
            });
        }

        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> config)
        {
            ServiceBusSettings settings = new ServiceBusSettings();
            configuration.Bind(nameof(ServiceBusSettings), settings);

            services.AddMassTransit(x =>
            {
                services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
                if (settings.AzureServiceBusEnabled)
                {
                    // "Endpoint=sb://crm-sample.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=XsZ6St5OJlf+NQ3aEXQ+R+3JuoHykb7TuKsnxhgb02Q=
                    x.UsingAzureServiceBus((context, cfg) =>
                    {
                        var connectionString = settings.EventBusConnection;
                        cfg.Host(connectionString);

                        cfg.ConfigureEndpoints(context);
                    });
                }
                else
                {
                    // docker run --rm -it --hostname crm-sample-queue -p 15672:15672 -p 5672:5672 rabbitmq:3-management
                    x.UsingRabbitMq((context, config) =>
                    {
                        // $"rabbitmq://{configuration["EventBusConfig:RabbitMq:HostAddress"]}"
                        //config.Host($"{settings.EventBusConnection}", "/");
                        config.Host("localhost", "/");
                        config.ConfigureEndpoints(context);
                    });
                }

                if (config != null)
                {
                    config(x);
                }
            });
        }

        /// <summary>
        /// Adds custom health checks to an api
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        /// <param name="configuration">The IConfiguration</param>
        /// <returns>
        /// The IServiceCollection
        /// </returns>
        /// <remarks>
        /// see: https://localhost:5001/hc-ui#/healthchecks
        /// </remarks>
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration, string uiDisplayName)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddCheck<ExternalEndpointHealthCheck>("Service availability");

            if (!configuration.GetValue<bool>("DatabaseSettings:UseInMemoryDatabase"))
            {
                hcBuilder.AddSqlServer(
                        configuration["ConnectionStrings:DatabaseConnectionString"],
                        name: "database-check",
                        tags: new string[] { "commshubdb", "ready" });
            }

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                hcBuilder.AddAzureServiceBusTopic(
                        configuration["EventBusConnection"],
                        topicName: "comms_hub_event_bus",
                        name: "azure-servicebus-check",
                        tags: new string[] { "servicebus", "ready" });
            }
            else
            {
                hcBuilder.AddRabbitMQ(
                        $"amqp://{configuration["EventBusConfig:RabbitMq:HostAddress"]}",
                        name: "rabbitmqbus-check",
                        tags: new string[] { "rabbitmqbus", "ready" });
            }

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(30);
                opt.MaximumHistoryEntriesPerEndpoint(100);

                opt.AddHealthCheckEndpoint(uiDisplayName, "/hc/ready");
            })
            .AddInMemoryStorage();

            return services;
        }
    }
}
