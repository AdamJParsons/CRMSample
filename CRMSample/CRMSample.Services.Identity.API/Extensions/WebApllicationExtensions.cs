using CRMSample.Domain.Identity.Entities.Account;
using CRMSample.Infrastructure.Identity.Persistence;
using Microsoft.AspNetCore.Identity;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebApllicationExtensions
    {
        public static async Task UseSeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // seed data, if required
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>>();

                    await DbContextInitialiser.InitialiseAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An errror occurred while migrating or seeding the database");
                    throw;
                }
            }
        }

        public static void UseCustomHealthChecks(this WebApplication app)
        {
            app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");
            app.MapHealthChecks("/hc/live", new HealthCheckOptions()
            {
                Predicate = _ => false,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.MapHealthChecks("/hc/ready", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("ready"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }

}
