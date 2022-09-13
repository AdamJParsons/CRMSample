using CRMSample.Domain.Admin.ViewModels.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Admin.User.Queries
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public GetUserQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
