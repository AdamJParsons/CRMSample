using CRMSample.Domain.Admin.Dtos.User;
using CRMSample.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Admin.Entities.Person
{
    public class PersonModel : EntityBase
    {
        public long? TitleId { get; set; }
        public long? GenderId { get; set; }
        public string Forename { get; set; }
        public string? OtherNames { get; set; }
        public string Surname { get; set; }

        public void UpdateFrom(UpdateUserDto updateUserDto, DateTimeOffset dateModified)
        {
            TitleId = updateUserDto.PersonTitleId;
            GenderId = updateUserDto.PersonGenderId;
            Forename = updateUserDto.PersonForename;
            OtherNames = updateUserDto.PersonOtherNames;
            Surname = updateUserDto.PersonSurname;

            DateModified = dateModified;
        }
    }
}
