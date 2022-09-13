using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Common.Entities
{
    public class EntityBase
    {
        public long Id { get; set; }
        public Guid IntegrationId { get; set; } = Guid.NewGuid();
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset DateModified { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
