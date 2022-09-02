using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Common.Dtos
{
    public record BaseReadDto
    {
        public long Id { get; init; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
