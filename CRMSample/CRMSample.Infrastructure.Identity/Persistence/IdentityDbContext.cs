using CRMSample.Application.Common.Services;
using CRMSample.Domain.Identity.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;

namespace CRMSample.Infrastructure.Identity.Persistence
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        private readonly IDateTime _dateTime;

        public IdentityDbContext(DbContextOptions options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
