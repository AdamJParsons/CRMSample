using CRMSample.Domain.Identity.Entities.Account;
using Microsoft.AspNetCore.Identity;

namespace CRMSample.Infrastructure.Identity.Persistence
{
    public class DbContextInitialiser
    {
        public static async Task InitialiseAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<long>> roleManager)
        {

            bool adminRoleExists = await roleManager.RoleExistsAsync("Administrator");
            bool userRoleExists = await roleManager.RoleExistsAsync("User");

            if (!adminRoleExists)
            {
                var adminRole = new IdentityRole<long>("Administrator");
                await roleManager.CreateAsync(adminRole);
            }

            if (!userRoleExists)
            {
                var userRole = new IdentityRole<long>("User");
                await roleManager.CreateAsync(userRole);
            }
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync("administrator@localhost");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    IntegrationId = new Guid("7E535C91-90C5-47FE-B468-1B598B28E4A2"), 
                    Email = "administrator@localhost",
                    NormalizedEmail = "administrator@localhost".ToUpperInvariant(),
                    UserName = "administrator@localhost",
                    NormalizedUserName = "administrator@localhost".ToUpperInvariant(),
                    SecurityStamp = "someRandomSecurityStamp"
                };

                await userManager.CreateAsync(adminUser, "Pa55word!");
                await userManager.AddToRoleAsync(adminUser, "Administrator");
            }
        }
    }
}
