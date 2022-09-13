using AutoMapper;

namespace CRMSample.Application.Tests.Common.Infrastructure
{
    public interface IAutoMapperFactory
    {
        IMapper Create();
    }
}
