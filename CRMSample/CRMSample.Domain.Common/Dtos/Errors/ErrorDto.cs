using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Common.Dtos.Errors
{
    public record ErrorDto
    {
        public ErrorDto(string description, string details)
        {
            Description = description;
            Details = details;
        }

        public string Description { get; }

        public string Details { get; }
    }
}
