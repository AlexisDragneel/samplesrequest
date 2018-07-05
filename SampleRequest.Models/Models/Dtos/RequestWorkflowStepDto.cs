using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Dtos
{
    public class RequestWorkflowStepBaseDto
    {
        public int id { get; set; }
        public string primary_responsible_id { get; set; }
        public string secondary_responsible_id { get; set; }
        public int order_workflow_step { get; set; }
        public string workflow_step_name { get; set; }
        public int workflow_step_id { get; set; }
        public string approved_by { get; set; }
        public DateTime? approved_date { get; set; }

    }
}
