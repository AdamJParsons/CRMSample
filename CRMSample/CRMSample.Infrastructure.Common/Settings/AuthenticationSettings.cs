using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Common.Settings
{
    public class AuthenticationSettings
    {
        public string AuthenticationKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Authority { get; set; }
        public int ExpiryTimeInMinutes { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
    }
}
