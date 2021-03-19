using App.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.IBLLManager
{
    public interface INewsPaperSettingsBLL
    {
        Task<ResponseMessage> GetNewsPaperSettingsRow();
    }
}
