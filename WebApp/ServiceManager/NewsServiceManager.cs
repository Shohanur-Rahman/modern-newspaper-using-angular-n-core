using App.BLL.IBLLManager;
using App.Common.Response;
using App.Models.VMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.IServiceManager;

namespace WebApp.ServiceManager
{
    public class NewsServiceManager : INewsServiceManager
    {
        private readonly INewsBLLManager _newsBLL;
        private readonly IUploaderHelper _uploader;
        public NewsServiceManager(INewsBLLManager newsBLL, IUploaderHelper uploader)
        {
            _newsBLL = newsBLL;
            _uploader = uploader;
        }
        public async Task<ResponseMessage> DeleteNews(long id)
        {
            return await _newsBLL.DeleteNews(id);
        }

        public async Task<ResponseMessage> DeleteNewsCategory(int id)
        {
            return await _newsBLL.DeleteNewsCategory(id);
        }

        public async Task<ResponseMessage> DeleteNewsTags(int id)
        {
            return await _newsBLL.DeleteNewsTags(id);
        }

        public async Task<ResponseMessage> GetAllNews()
        {
            return await _newsBLL.GetAllNews();
        }

        public async Task<ResponseMessage> GetAllNewsCategories()
        {
            return await _newsBLL.GetAllNewsCategories();
        }

        public async Task<ResponseMessage> GetAllNewsTags()
        {
            return await _newsBLL.GetAllNewsTags();
        }

        public async Task<ResponseMessage> GetNewCattegoryTree()
        {
            return await _newsBLL.GetNewCattegoryTree();
        }

        public async Task<ResponseMessage> GetNewsById(long id)
        {
            return await _newsBLL.GetNewsById(id);
        }

        public async Task<ResponseMessage> GetNewsCategoryById(int id)
        {
            return await _newsBLL.GetNewsCategoryById(id);
        }

        public async Task<ResponseMessage> GetNewsTagsById(int id)
        {
            return await _newsBLL.GetNewsTagsById(id);
        }

        public async Task<ResponseMessage> SaveUpdateNews(VMNewsModel model)
        {
            try
            {
                if(model.Image !=null && model.Image.Length > 0)
                {
                    model.FeaturedImage = _uploader.UploadFile(model.Image, "news");
                }

                if (model.Id > 0) {
                    model.EditedDate = DateTime.Now;
                    return await _newsBLL.UpdateNews(model);
                }
                model.CreatedDate = DateTime.Now;
                return await _newsBLL.SaveNews(model);

            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                throw;
            }
            
        }

        public async Task<ResponseMessage> SaveUpdateNewsCategory(VMNewsChildModel model)
        {
            if (model.Id > 0)
                return await _newsBLL.UpdateNewsCategory(model);

            return await _newsBLL.SaveNewsCategory(model);
        }

        public async Task<ResponseMessage> SaveUpdateNewsTags(VMNewsChildModel model)
        {
            if (model.Id > 0)
                return await _newsBLL.UpdateNewsTags(model);

            return await _newsBLL.SaveNewsTags(model);
        }
    }
}
