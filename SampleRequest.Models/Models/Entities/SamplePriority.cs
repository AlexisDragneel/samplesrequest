using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.Entities
{
    
    public class SamplePriority : Catalog
    {
        public virtual ICollection<SampleRequest> sample_requests { get; set; }
    }
}
