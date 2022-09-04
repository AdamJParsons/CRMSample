using CRMSample.Infrastructure.Common.Middleware.HealthChecks;
using CRMSample.Infrastructure.Common.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        //public static void SetupCustomEventBus(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollectionBusConfigurator> config)
        //{
        //    // docker command
        //    // docker run --rm -it --hostname my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
        //    services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        //    services.AddMassTransit(options =>
        //    {
        //        // maybe azure service bus....
        //        options.UsingRabbitMq((context, config) =>
        //        {
        //            config.Host($"rabbitmq://{configuration["EventBusConfig:RabbitMq:HostAddress"]}");
        //            config.ConfigureEndpoints(context);
        //        });
        //        if (config != null)
        //        {
        //            config(options);
        //        }
        //    });
        //    services.AddMassTransitHostedService();
        //}

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

            hcBuilder.AddSqlServer(
                    configuration["ConnectionStrings:DatabaseConnectionString"],
                    name: "database-check",
                    tags: new string[] { "commshubdb", "ready" });

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
