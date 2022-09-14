using CRMSample.Infrastructure.Admin.Persistence;
using Microsoft.AspNetCore.Identity;

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
                    var dbContext = services.GetRequiredService<AdminDbContext>();

                    if (app.Environment.IsDevelopment())
                    {
                        await dbContext.Database.EnsureCreatedAsync();
                    }

                    await DbContextInitialiser.InitialiseAsync(dbContext);
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