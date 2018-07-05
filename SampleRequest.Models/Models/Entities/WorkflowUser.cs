using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("workflow_users")]
    public class WorkflowUser
    {
        public int id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string primary_responsible_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string primary_responsible_name { get; set; }

        public int workflow_step_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string secondary_responsible_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string secondary_responsible_name { get; set; }

        [ForeignKey("primary_responsible_id")]
        public virtual User primary_responsible { get; set; }

        [ForeignKey("secundary_responsible_id")]
        public virtual User secondary_responsible { get; set; }

        [ForeignKey("workflow_step_id")]
        public virtual WorkflowStep workflow_step { get; set; }
    }
}
