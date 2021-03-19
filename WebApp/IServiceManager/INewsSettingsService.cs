using App.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface INewsSettingsService
    {
        Task<ResponseMessage> GetNewsPaperSettingsRow();
    }
}
