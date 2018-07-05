using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Security.Models.Entities
{
    [Table("user_roles")]
    public class UserRole
    {
        public string uname { get; set; }
        public int role_id { get; set; }

        [ForeignKey("uname")]
        public User user { get; set; }

        [ForeignKey("role_id")]
        public Role role { get; set; }
    }
}
