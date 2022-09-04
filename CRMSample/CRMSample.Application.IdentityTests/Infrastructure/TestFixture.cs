using AutoMapper;
using CRMSample.Application.Identity.Services;
using MediatR;
using Moq;

namespace CRMSample.Application.IdentityTests.Infrastructure
{
    public class TestFixture
    {
        public IMediator Mediator => MockMediator.Object;
        public Mock<IMediator> MockMediator { get; set; } = new Mock<IMediator>();

        public ILoginService LoginService => MockLoginService.Object;
        public Mock<ILoginService> MockLoginService { get; set; } = new Mock<ILoginService>();

        public ITokenService TokenService => MockTokenService.Object;
        public Mock<ITokenService> MockTokenService { get; set; } = new Mock<ITokenService>();

        public IMapper Mapper { get; set; }

        public TestFixture()
        {
            Mapper = AutoMapperFactory.Create();
        }
    }
}
