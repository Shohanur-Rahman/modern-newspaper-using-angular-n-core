using App.BLL.IBLLManager;
using App.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IServiceManager;

namespace WebApp.ServiceManager
{
    public class NewsFrontServiceManager: INewsFrontService
    {
        private readonly IFrontNewsBLLManager _newsFront;
        public NewsFrontServiceManager(IFrontNewsBLLManager newsFront)
        {
            _newsFront = newsFront;
        }

        public async Task<ResponseMessage> GetBreakingNews()
        {
            return await _newsFront.GetBreakingNews();
        }

        public async Task<ResponseMessage> GetNewsDetailsByCategoryAndTitle(string catSlug, string titleSlug)
        {
            return await _newsFront.GetNewsDetailsByCategoryAndTitle(catSlug, titleSlug);
        }

        public async Task<ResponseMessage> GetRelatedNewsByCategory(long categoryId)
        {
            return await _newsFront.GetRelatedNewsByCategory(categoryId);
        }
    }
}
