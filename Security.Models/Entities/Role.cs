using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Security.Models.Entities
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int area_id { get; set; }
        public  long squareId { get; set; }

        public ICollection<UserRole> users { get; set; }
    }
}
