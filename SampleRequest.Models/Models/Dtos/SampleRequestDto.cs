using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Dtos
{
    public class SampleRequestBaseDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "The project name is required")]
        public string project_name { get; set; }

        [Required(ErrorMessage = "The objective of the request is required")]
        public string objective { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        public bool require_test { get; set; }

        [Required(ErrorMessage = "You need to select a purpose for the request")]
        [Range(1, int.MaxValue)]
        public int sample_purpose_id { get; set; }

        [Required(ErrorMessage = "You need to select a priority for the reques")]
        [Range(1, int.MaxValue)]
        public int sample_priority_id { get; set; }

        public string comments { get; set; }

    }
    public class SampleRequestDto : SampleRequestBaseDto
    {
        

        public virtual List<SampleRequestDetailBaseDto> sample_request_details { get; set; }

        public virtual SamplePriorityBaseDto priority { get; set; }

        public virtual AddressDto address { get; set; }

        public virtual List<RequestWorkflowStepBaseDto> request_workflow_steps { get; set; }

    }

    public class SampleRequestWithStatusDto : SampleRequestDto
    {
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
