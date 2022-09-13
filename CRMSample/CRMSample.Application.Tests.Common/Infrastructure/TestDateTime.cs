using CRMSample.Application.Common.Services;

namespace CRMSample.Application.Tests.Common.Infrastructure
{
    public class TestDateTime : IDateTime
    {
        private DateTime _dateTime;

        public TestDateTime()
        {
            _dateTime = new DateTime(2022,1,1);
        }

        public DateTime Now => _dateTime;

        public int CurrentYear => _dateTime.Year;

        public int CurrentMonth => _dateTime.Month;

        public int CurrentDay => _dateTime.Day;
    }
}
