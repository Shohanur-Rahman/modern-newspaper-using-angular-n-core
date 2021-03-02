using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.IBLLManager
{
    public interface INewsBLLManager
    {
        /// <summary>
        /// News Categories
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage> GetAllNewsCategories();
        Task<ResponseMessage> SaveNewsCategory(VMNewsChildModel model);
        Task<ResponseMessage> UpdateNewsCategory(VMNewsChildModel model);
        Task<ResponseMessage> DeleteNewsCategory(int id);
        Task<ResponseMessage> GetNewsCategoryById(int id);
        Task<ResponseMessage> GetNewCattegoryTree();
        /// <summary>
        /// News Tags
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage> GetAllNewsTags();
        Task<ResponseMessage> SaveNewsTags(VMNewsChildModel model);
        Task<ResponseMessage> UpdateNewsTags(VMNewsChildModel model);
        Task<ResponseMessage> DeleteNewsTags(int id);
        Task<ResponseMessage> GetNewsTagsById(int id);
        /// <summary>
        /// News
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage> GetAllNews();
        Task<ResponseMessage> SaveNews(VMNewsModel model);
        Task<ResponseMessage> UpdateNews(VMNewsModel model);
        Task<ResponseMessage> DeleteNews(long id);
        Task<ResponseMessage> GetNewsById(long id);
    }
}
