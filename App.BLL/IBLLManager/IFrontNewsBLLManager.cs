using App.Common.Response;
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
    }
}
