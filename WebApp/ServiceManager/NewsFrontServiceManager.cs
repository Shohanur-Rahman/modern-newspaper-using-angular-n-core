using App.BLL.IBLLManager;
using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IServiceManager;

namespace WebApp.ServiceManager
{
    public class NewsFrontServiceManager : INewsFrontService
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

        public async Task<ResponseMessage> GetVideoNews(int perPage, int pageNumber)
        {
            return await _newsFront.GetVideoNews(perPage, pageNumber);
        }

        public async Task<ResponseMessage> RecentThreeNews()
        {
            return await _newsFront.RecentThreeNews();
        }

        public async Task<ResponseMessage> SaveUpdateNewsComment(VMNewsComments model)
        {
            if (model.Id > 0)
                return await _newsFront.UpdateNewsComment(model);
            else
                return await _newsFront.SaveNewsComment(model);
        }
    }
}
