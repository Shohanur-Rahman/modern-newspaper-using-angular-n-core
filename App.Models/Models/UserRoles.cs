using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("UserRoles")]
    public class UserRole
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}
