using AutoMapper;
using CRMSample.Application.Admin.Data;
using CRMSample.Application.Tests.Common.Infrastructure;

namespace CRMSample.Application.AdminTests.Infrastructure
{
    public class AutoMapperFactory : IAutoMapperFactory
    {
        public IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(configuration => configuration.AddProfile(new MappingProfile()));
            return mappingConfig.CreateMapper();
        }
    }
}
