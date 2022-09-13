using AutoMapper;
using CRMSample.Application.Common.Services;
using MediatR;
using Moq;

namespace CRMSample.Application.Tests.Common.Infrastructure
{
    public abstract class TestFixtureBase
    {
        public IMediator Mediator => MockMediator.Object;
        public Mock<IMediator> MockMediator { get; set; } = new Mock<IMediator>();

        public IDateTime MachineDateTime { get; } = new TestDateTime();

        public abstract IMapper Mapper { get; }

        public TestFixtureBase()
        {
        }
    }
}
