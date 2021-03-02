using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface IUploaderHelper
    {
        string UploadFile(IFormFile file, string directoryName);
    }
}
