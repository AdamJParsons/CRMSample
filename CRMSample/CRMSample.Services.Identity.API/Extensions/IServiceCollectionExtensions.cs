using CRMSample.Domain.Identity.Entities.Account;
using CRMSample.Infrastructure.Common.Mediatr;
using CRMSample.Infrastructure.Identity.Persistence;
using MediatR.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CRMSample.Application.Identity.Account.Commands.Login;
using CRMSample.Infrastructure.Common.Settings;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<long>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = new DatabaseSettings();
            configuration.Bind(nameof(DatabaseSettings), databaseSettings);

            if(databaseSettings != null && databaseSettings.UseInMemoryDatabase)
            {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseInMemoryDatabase("CRM_SAMPLE_AUTH"));
            }
            else
            {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DatabaseConnectionString"),
                        b => b.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)));
            }
        }

        public static void AddMediator(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            services.AddMediatR(typeof(LoginCommand).Assembly);
        }
    }

}
