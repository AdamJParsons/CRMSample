using AutoMapper;
using CRMSample.Application.Identity.Services;
using MediatR;
using Moq;

namespace CRMSample.Application.IdentityTests.Account.Commands
{
    public class TestFixture
    {
        public IMediator Mediator => MockMediator.Object;
        public Mock<IMediator> MockMediator { get; set; }

        public ILoginService LoginService => MockLoginService.Object;
        public Mock<ILoginService> MockLoginService { get; set; }

        public IMapper Mapper { get; set; }

        public TestFixture()
        {
            MockMediator = new Mock<IMediator>();
            MockLoginService = new Mock<ILoginService>();

            Mapper = AutoMapperFactory.Create();
        }
    }
}
