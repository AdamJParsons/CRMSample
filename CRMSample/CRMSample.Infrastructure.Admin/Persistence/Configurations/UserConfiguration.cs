using CRMSample.Domain.Admin.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Admin.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUserModel>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserModel> builder)
        {
            builder
                .Property(x => x.UserName)
                .HasMaxLength(256)
                .IsRequired();

            builder
                .Property(x => x.EmailAddress)
                .HasMaxLength(256)
                .IsRequired();

            builder
                .Property(x => x.JobTitle)
                .HasMaxLength(256);
        }
    }
}
