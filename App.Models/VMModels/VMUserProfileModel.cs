using App.Models.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMUserProfileModel: UserProfile
    {
        public IFormFile NIDImage { get; set; }
    }
}
