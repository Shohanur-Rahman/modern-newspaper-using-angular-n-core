using App.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.IServiceManager
{
    public interface ICommentsServiceManager
    {
        Task<ResponseMessage> GetAllComments();
        Task<ResponseMessage> GetNewsCommentsById(long commentsId);
        Task<ResponseMessage> DeleteNewsCommentsById(long commentsId);
    }
}
