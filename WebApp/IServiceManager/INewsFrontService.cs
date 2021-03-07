using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface INewsFrontService
    {
        Task<ResponseMessage> GetBreakingNews();
        Task<ResponseMessage> GetNewsDetailsByCategoryAndTitle(string catSlug, string titleSlug);
        Task<ResponseMessage> GetRelatedNewsByCategory(long categoryId);
        Task<ResponseMessage> SaveUpdateNewsComment(VMNewsComments model);
        Task<ResponseMessage> RecentThreeNews();
    }
}
