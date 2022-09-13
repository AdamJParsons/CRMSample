using AutoMapper;
using CRMSample.Application.Admin.Services;
using CRMSample.Application.Tests.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.AdminTests.Infrastructure
{
    public class TestFixture : TestFixtureBase
    {
        public IUserService UserService => MockUserService.Object;
        public Mock<IUserService> MockUserService { get; set; } = new Mock<IUserService>();

        public override IMapper Mapper { get; }

        public TestFixture()
        {
            Mapper = new AutoMapperFactory().Create();
        }
    }
}
