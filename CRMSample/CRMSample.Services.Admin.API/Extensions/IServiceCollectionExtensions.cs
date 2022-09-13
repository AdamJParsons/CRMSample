using CRMSample.Infrastructure.Common.Mediatr;
using MediatR.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CRMSample.Infrastructure.Admin.Persistence;
using CRMSample.Application.Admin.User.Commands.CreateUser;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            //services.AddIdentity<ApplicationUser, IdentityRole<long>>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //})
            //.AddEntityFrameworkStores<IdentityDbContext>()
            //.AddDefaultTokenProviders();
        }

        public static void AddCustomDbContext(this IServiceCollection services)
        {
            //builder.Services.AddDbContext<IdentityDbContext>(options =>
            //    options.UseSqlServer(
            //        builder.Configuration.GetConnectionString("DatabaseConnectionString"),
            //        b => b.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)));

            services.AddDbContext<AdminDbContext>(options =>
                options.UseInMemoryDatabase("CRM_SAMPLE_ADMIN"));
        }

        public static void AddMediator(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            services.AddMediatR(typeof(CreateUserCommand).Assembly);
        }
    }

}
