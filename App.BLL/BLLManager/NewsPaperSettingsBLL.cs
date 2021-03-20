using App.BLL.IBLLManager;
using App.Common.Response;
using App.Models.AppContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using App.Models.VMModels;
using Microsoft.EntityFrameworkCore;
using App.Common.Static;
using App.Common.Enums;
using App.Common.Constant;
using App.Models.Models;

namespace App.BLL.BLLManager
{
    public class NewsPaperSettingsBLL: INewsPaperSettingsBLL
    {
        private readonly ApplicationDbContext _context;
        public NewsPaperSettingsBLL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseMessage> GetNewsPaperSettingsRow()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await(from settings in _context.NewsPortalSettings
                                       select new VMNewsPaperSettings()
                                       {
                                           Id = settings.Id,
                                           HomeNewsCategory1 = settings.HomeNewsCategory1,
                                           HomeNewsCategory2 = settings.HomeNewsCategory2,
                                           HomeNewsCategory3 = settings.HomeNewsCategory3
                                       }).FirstOrDefaultAsync();

                if (listOfNews == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                return result = ResponseMapping.GetResponseMessage(listOfNews, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> SaveUpdateCategorySettings(VMNewsPaperSettings model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var existingSettings = await _context.NewsPortalSettings.FirstOrDefaultAsync();

                if (existingSettings == null)
                {
                    NewsPortalSetting settings = new NewsPortalSetting();
                    settings.HomeNewsCategory1 = model.HomeNewsCategory1;
                    settings.HomeNewsCategory2 = model.HomeNewsCategory2;
                    settings.HomeNewsCategory3 = model.HomeNewsCategory3;
                    _context.NewsPortalSettings.Add(settings);
                    await _context.SaveChangesAsync();

                    return result = ResponseMapping.GetResponseMessage(settings, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }
                else
                {
                    existingSettings.HomeNewsCategory1 = model.HomeNewsCategory1;
                    existingSettings.HomeNewsCategory2 = model.HomeNewsCategory2;
                    existingSettings.HomeNewsCategory3 = model.HomeNewsCategory3;
                    await _context.SaveChangesAsync();
                    return result = ResponseMapping.GetResponseMessage(existingSettings, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }
    }
}
