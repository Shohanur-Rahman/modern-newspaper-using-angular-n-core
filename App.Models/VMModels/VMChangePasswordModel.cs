using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMChangePasswordModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public int AttemptCount { get; set; }
        public string Password { get; set; }
    }
}
