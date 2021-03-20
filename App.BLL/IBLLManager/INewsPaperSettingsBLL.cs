using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.IBLLManager
{
    public interface INewsPaperSettingsBLL
    {
        Task<ResponseMessage> GetNewsPaperSettingsRow();
        Task<ResponseMessage> SaveUpdateCategorySettings(VMNewsPaperSettings model);
    }
}
