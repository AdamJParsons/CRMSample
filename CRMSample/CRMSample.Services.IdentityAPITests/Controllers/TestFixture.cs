using MediatR;
using Moq;

namespace CRMSample.Services.IdentityAPITests.Controllers
{
    public class TestFixture
    {
        public IMediator Mediator => MockMediator.Object;
        public Mock<IMediator> MockMediator { get; set; }

        public TestFixture()
        {
            MockMediator = new Mock<IMediator>();
        }
    }
}
