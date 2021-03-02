using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface INewsServiceManager
    {
        /// <summary>
        /// News Categories
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage> GetAllNewsCategories();
        Task<ResponseMessage> SaveUpdateNewsCategory(VMNewsChildModel model);
        Task<ResponseMessage> DeleteNewsCategory(int id);
        Task<ResponseMessage> GetNewsCategoryById(int id);
        Task<ResponseMessage> GetNewCattegoryTree();
        /// <summary>
        /// News Tags
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage> GetAllNewsTags();
        Task<ResponseMessage> SaveUpdateNewsTags(VMNewsChildModel model);
        Task<ResponseMessage> DeleteNewsTags(int id);
        Task<ResponseMessage> GetNewsTagsById(int id);
        /// <summary>
        /// News
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage> GetAllNews();
        Task<ResponseMessage> SaveUpdateNews(VMNewsModel model);
        Task<ResponseMessage> DeleteNews(long id);
        Task<ResponseMessage> GetNewsById(long id);
    }
}
