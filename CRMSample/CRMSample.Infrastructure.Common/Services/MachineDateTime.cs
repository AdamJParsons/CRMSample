using CRMSample.Application.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Common.Services
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;

        public int CurrentYear => DateTimeOffset.Now.Year;

        public int CurrentMonth => DateTimeOffset.Now.Month;

        public int CurrentDay => DateTimeOffset.Now.Day;
    }
}
