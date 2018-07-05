using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("request_workflow_steps")]
    public class RequestWorkflowStep
    {
        public int id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string primary_responsible_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string secondary_responsible_id { get; set; }

        [Required]
        public int order_workflow_step { get; set; }

        public string approved_by { get; set; }

        public DateTime? approved_date { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string workflow_step_name { get; set; }

        public int workflow_step_id { get; set; }

        public int sample_request_id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string comments { get; set; }


        [ForeignKey("primary_responsible_id")]
        public virtual User primary_responsible { get; set; }

        [ForeignKey("secondary_responsible_id")]
        public virtual User secondary_responsible { get; set; }

        [ForeignKey("workflow_step_id")]
        public virtual WorkflowStep Workflow_step { get; set; }

        [ForeignKey("sample_request_id")]
        public virtual SampleRequest sample_request { get; set; }

    }
}
