using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("sample_requests")]
    public class SampleRequest
    {

        public int id { get; set; }

        public DateTime created_at { get; set; }

        [Required]
        public int created_by { get; set; }

        public int? address_id { get; set; }

        [Required]
        public int sample_purpose_id { get; set; }

        [Required]
        public int sample_priority_id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string project_name { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string objective { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string comments { get; set; }

        public bool require_test { get; set; }

        [ForeignKey("sample_purpose_id")]
        public SamplePurpose purpose { get; set; }

        [ForeignKey("sample_priority_id")]
        public SamplePriority priority { get; set; }

        [ForeignKey("address_id")]
        public Address address { get; set; }

        public virtual ICollection<SampleRequestDetail> sample_request_details { get; set; }

        public virtual ICollection<RequestWorkflowStep> request_workflow_steps { get; set; }

        public virtual string current_status
        {
            get
            {
                return request_workflow_steps.OrderBy(e => e.order_workflow_step).FirstOrDefault(e => e.approved_date == null).workflow_step_name;
            }
        }

        public virtual int current_percentage
        {
            get
            {
                return request_workflow_steps.Where(e => e.approved_date != null).Count() / request_workflow_steps.Count();
            }
        }
    }
}
