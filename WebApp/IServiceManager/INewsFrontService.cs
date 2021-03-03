using App.Common.Response;
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
    }
}
