using CRMSample.Application.Common.Services;
using CRMSample.Domain.Admin.Entities.User;
using CRMSample.Domain.Common.Entities;
using CRMSample.Infrastructure.Common.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Admin.Persistence
{
    public class AdminDbContext : DbContext, IUnitOfWork
    {
        private readonly IDateTime _dateTime;

        public DbSet<ApplicationUserModel> Users => Set<ApplicationUserModel>();

        public AdminDbContext(DbContextOptions options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DateModified = _dateTime.Now;
                        entry.Entity.DateDeleted = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateModified = _dateTime.Now;
                        break;
                    case EntityState.Added:
                        entry.Entity.DateCreated = _dateTime.Now;
                        entry.Entity.DateModified = _dateTime.Now;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
