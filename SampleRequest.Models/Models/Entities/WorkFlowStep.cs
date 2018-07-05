using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("workflow_steps")]
    public class WorkflowStep
    {
        public int id { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string name { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string description { get; set; }
        public bool only_if_lab_test { get; set; }
        public int order { get; set; }

        public virtual ICollection<WorkflowUser> responsibles { get; set; }

    }
}
