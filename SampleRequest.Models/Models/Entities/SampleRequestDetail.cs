using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("sample_request_details")]
    public class SampleRequestDetail
    {
        public int id { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string model_number { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string description { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string specs { get; set; }

        public int request_id { get; set; }

        [ForeignKey("request_id")]
        public virtual SampleRequest request { get; set; }
    }
}
