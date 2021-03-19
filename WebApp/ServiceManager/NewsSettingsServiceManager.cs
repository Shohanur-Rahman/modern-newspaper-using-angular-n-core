using App.BLL.IBLLManager;
using App.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IServiceManager;

namespace WebApp.ServiceManager
{
    public class NewsSettingsServiceManager : INewsSettingsService
    {
        private readonly INewsPaperSettingsBLL _settingsBLL;
        public NewsSettingsServiceManager(INewsPaperSettingsBLL settingsBLL)
        {
            _settingsBLL = settingsBLL;
        }
        public async Task<ResponseMessage> GetNewsPaperSettingsRow()
        {
            return await _settingsBLL.GetNewsPaperSettingsRow();
        }
    }
}
