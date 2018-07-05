using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamplesRequest.Models.Models.Entities
{
    [Table("clients")]
    public class Client
    {
        public int id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string name { get; set; }

        public virtual ICollection<Address> addresses { get; set; }
    }
}
