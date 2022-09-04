using AutoMapper;
using CRMSample.Application.Identity.Data;

namespace CRMSample.Application.IdentityTests.Infrastructure
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(configuration => configuration.AddProfile(new MappingProfile()));
            return mappingConfig.CreateMapper();
        }
    }
}
