using App.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.IBLLManager
{
    public interface ICommentsBLLManager
    {
        Task<ResponseMessage> GetAllComments();
        Task<ResponseMessage> GetNewsCommentsById(long commentsId);
        Task<ResponseMessage> DeleteNewsCommentsById(long commentsId);
    }
}
