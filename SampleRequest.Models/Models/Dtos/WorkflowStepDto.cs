using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Dtos
{
    public class WorkflowStepBaseDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool only_if_lab_test { get; set; }
        public int order { get; set; }
    }

    public class WorkflowStepWithResponsiblesDto : WorkflowStepBaseDto
    {
        public List<WorkflowUserBaseDto> responsibles { get; set; }
    }
}
