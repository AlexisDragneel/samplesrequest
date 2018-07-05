using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("catalogs")]
    public class Catalog
    {
        public int id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string name { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string key { get; set; }
    }
}
