using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("USERS")]
    public class Users : BaseTableInfo
    {
        [Key]
        public long Id { get; set; }
        [DefaultValue(3)]
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsLock { get; set; }
    }
}
