using App.BLL.IBLLManager;
using App.Common.Constant;
using App.Common.Enums;
using App.Common.Response;
using App.Common.Static;
using App.Models.AppContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using App.Models.VMModels;
using Microsoft.EntityFrameworkCore;

namespace App.BLL.BLLManager
{
    public class FrontNewsBLLManager: IFrontNewsBLLManager
    {
        private readonly ApplicationDbContext _context;
        public FrontNewsBLLManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseMessage> GetBreakingNews()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await (from news in _context.NewsPost
                                        where news.IsBreaking == true
                                        select new VMBreakingNews()
                                        {
                                            Id = news.Id,
                                            Title = news.Title,
                                            CategorySlug =  (from sl in _context.NewsCategories where sl.Id == news.CategoryId select sl.Slug).FirstOrDefault(),
                                            TitleSlug= news.TitleSlug
                                        }).ToListAsync();

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
