using CRMSample.Domain.Admin.Dtos.User;
using CRMSample.Domain.Admin.Entities.Person;
using CRMSample.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Admin.Entities.User
{
    public class ApplicationUserModel : EntityBase
    {
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string? JobTitle { get; set; }

        public PersonModel Person { get; set; } = new PersonModel();

        public void UpdateFrom(UpdateUserDto updateDto, DateTimeOffset dateModified)
        {
            DateModified = dateModified;

            EmailAddress = updateDto.EmailAddress;
            UserName = updateDto.UserName;
            JobTitle = updateDto.JobTitle;

            Person?.UpdateFrom(updateDto, dateModified);
        }
    }
}
