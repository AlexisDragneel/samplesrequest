using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string uname { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string name { get; set; }
        [Required]
        [Column(TypeName = "varchar(60)")]
        public string email { get; set; }
    }
}
