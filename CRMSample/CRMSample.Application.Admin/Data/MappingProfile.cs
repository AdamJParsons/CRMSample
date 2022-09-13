using AutoMapper;
using CRMSample.Domain.Admin.Dtos.User;
using CRMSample.Domain.Admin.Entities.User;

namespace CRMSample.Application.Admin.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUserModel, ReadUserDto>();
        }
    }
}
