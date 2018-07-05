using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Dtos
{
    public class WorkflowUserBaseDto
    {
        public int id { get; set; }
        public string primary_responsible_id { get; set; }
        public string primary_responsible_name { get; set; }

        public string secondary_responsible_id { get; set; }
        public string secondary_responsible_name { get; set; }

    }
}
