using AutoMapper;
using CRMSample.Application.Common.Services;
using CRMSample.Application.Identity.Services;
using CRMSample.Application.Tests.Common.Infrastructure;
using MediatR;
using Moq;

namespace CRMSample.Application.IdentityTests.Infrastructure
{
    public class TestFixture : TestFixtureBase
    {
        public ILoginService LoginService => MockLoginService.Object;
        public Mock<ILoginService> MockLoginService { get; set; } = new Mock<ILoginService>();

        public ITokenService TokenService => MockTokenService.Object;
        public Mock<ITokenService> MockTokenService { get; set; } = new Mock<ITokenService>();

        public override IMapper Mapper { get; }

        public TestFixture()
        {
            Mapper = new AutoMapperFactory().Create();
        }
    }
}
