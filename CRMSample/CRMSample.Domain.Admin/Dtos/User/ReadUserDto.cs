using CRMSample.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Admin.Dtos.User
{
    public record ReadUserDto : BaseReadDto
    {
        public Guid IntegrationId { get; init; }
        public string EmailAddress { get; init; }
        public string UserName { get; init; }
        public string JobTitle { get; init; }

        #region Person

        public long? PersonTitleId { get; set; }
        public long? PersonGenderId { get; set; }
        public string PersonForename { get; set; }
        public string PersonOtherNames { get; set; }
        public string PersonSurname { get; set; }

        #endregion
    }
}
