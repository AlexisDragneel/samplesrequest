using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("addresses")]
    public class Address
    {
        public int id { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string street_number { get; set; }


        [Required]
        [Column(TypeName = "varchar(10)")]
        public string zip { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string facility { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string phone { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string country { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string state { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string city { get; set; }

        public int client_id { get; set; }

        [ForeignKey("client_id")]
        public virtual Client client { get; set; }
    }
}
