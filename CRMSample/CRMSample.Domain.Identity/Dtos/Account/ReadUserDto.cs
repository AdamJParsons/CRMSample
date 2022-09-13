using CRMSample.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Identity.Dtos.Account
{
    public record ReadUserDto : BaseReadDto
    {
        public Guid IntegrationId { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
    }
}
