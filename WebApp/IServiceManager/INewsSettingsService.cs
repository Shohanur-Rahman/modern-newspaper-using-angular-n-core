using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface INewsSettingsService
    {
        Task<ResponseMessage> GetNewsPaperSettingsRow();
        Task<ResponseMessage> SaveUpdateCategorySettings(VMNewsPaperSettings model);
    }
}
