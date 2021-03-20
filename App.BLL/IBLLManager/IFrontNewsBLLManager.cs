using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.IBLLManager
{
    public interface IFrontNewsBLLManager
    {
        Task<ResponseMessage> GetBreakingNews();
        Task<ResponseMessage> GetNewsDetailsByCategoryAndTitle(string catSlug, string titleSlug);
        Task<ResponseMessage> GetRelatedNewsByCategory(long categoryId);
        Task<ResponseMessage> SaveNewsComment(VMNewsComments model);
        Task<ResponseMessage> UpdateNewsComment(VMNewsComments model);
        Task<ResponseMessage> RecentThreeNews();
        Task<ResponseMessage> GetVideoNews(int perPage, int pageNumber);
        Task<ResponseMessage> GetHomeCategoryNews();
    }
}
