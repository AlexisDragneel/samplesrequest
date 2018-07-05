using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Security.Models.Entities
{
    public class User
    {
        [Key]
        public string uname { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public Boolean active { get; set; }
        public Decimal naccess { get; set; }

        public ICollection<UserRole> roles { get; set; }
    }
}
