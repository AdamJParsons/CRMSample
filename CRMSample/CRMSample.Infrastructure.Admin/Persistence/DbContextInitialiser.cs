using CRMSample.Domain.Admin.Entities.Person;
using CRMSample.Domain.Admin.Entities.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CRMSample.Infrastructure.Admin.Persistence
{
    public class DbContextInitialiser
    {
        public static async Task InitialiseAsync(AdminDbContext context)
        {
            await SeedUsersAsync(context);
        }

        private static async Task SeedUsersAsync(AdminDbContext context)
        {
            var adminUser = await
                context
                    .Users
                    .SingleOrDefaultAsync(x => x.UserName.Equals("administrator@localhost", StringComparison.OrdinalIgnoreCase));

            if (adminUser == null)
            {
                adminUser = new ApplicationUserModel
                {
                    EmailAddress = "administrator@localhost",
                    UserName = "administrator@localhost",
                    JobTitle = "System Administrator",
                    IntegrationId = new Guid("7E535C91-90C5-47FE-B468-1B598B28E4A2"),
                    Person = new PersonModel
                    {
                        Forename = "Administrator",
                        Surname = "Administrator"
                    }
                };

                await context.Users.AddAsync(adminUser);
            }

            await context.SaveChangesAsync();
        }
    }
}
