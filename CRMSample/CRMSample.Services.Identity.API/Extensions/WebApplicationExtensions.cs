using CRMSample.Domain.Identity.Entities.Account;
using CRMSample.Infrastructure.Identity.Persistence;
using Microsoft.AspNetCore.Identity;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebApplicationExtensions
    {
        public static async Task UseSeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // seed data, if required
                    var dbContext = services.GetRequiredService<IdentityDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>>();

                    if (app.Environment.IsDevelopment())
                    {
                        await dbContext.Database.EnsureCreatedAsync();
                    }

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
    }
}
