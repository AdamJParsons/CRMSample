using AutoMapper;
using CRMSample.Domain.Identity.Dtos.Account;
using CRMSample.Domain.Identity.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Identity.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ReadUserDto>();
        }
    }
}
