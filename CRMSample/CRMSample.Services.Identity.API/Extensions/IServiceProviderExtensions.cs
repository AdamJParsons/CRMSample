﻿using CRMSample.Application.Identity.Account.Commands;
using CRMSample.Domain.Identity.Entities.Account;
using CRMSample.Infrastructure.Common.Mediatr;
using CRMSample.Infrastructure.Identity.Persistence;
using MediatR.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceProviderExtensions
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

        public static void AddCustomDbContext(this IServiceCollection services)
        {
            //builder.Services.AddDbContext<IdentityDbContext>(options =>
            //    options.UseSqlServer(
            //        builder.Configuration.GetConnectionString("DatabaseConnectionString"),
            //        b => b.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)));

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseInMemoryDatabase("CRM_SAMPLE_AUTH"));
        }

        public static void AddMediator(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            services.AddMediatR(typeof(LoginCommand).Assembly);
        }
    }

}
