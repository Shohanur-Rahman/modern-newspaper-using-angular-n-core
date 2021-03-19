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
    }
}
