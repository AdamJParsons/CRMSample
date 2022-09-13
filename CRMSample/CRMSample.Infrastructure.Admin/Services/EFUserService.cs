using CRMSample.Application.Admin.Services;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Domain.Admin.Entities.User;
using CRMSample.Infrastructure.Admin.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Admin.Services
{
    public class EFUserService : IUserService
    {
        private readonly AdminDbContext _context;

        public EFUserService(AdminDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ApplicationUserModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.Users.AddAsync(model, cancellationToken);
        }

        public async Task<ApplicationUserModel?> GetUserByEmailAsync(string emailAddress, CancellationToken cancellationToken = default)
        {
            var user = await _context
                .Users
                .SingleOrDefaultAsync(x => x.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase), cancellationToken);

            return user;
        }

        public async Task<ApplicationUserModel?> GetUserByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var user = await _context
                .Users
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return user;
        }

        public async Task<ApplicationUserModel?> GetUserByUserNameAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _context
                .Users
                .SingleOrDefaultAsync(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase), cancellationToken);

            return user;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
