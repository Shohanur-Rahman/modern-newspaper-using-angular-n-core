using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMUserModel
    {
        public long Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsLock { get; set; }
        public long? CreatedId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? EditedId { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
