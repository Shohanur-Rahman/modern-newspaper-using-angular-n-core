using App.BLL.IBLLManager;
using App.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IServiceManager;

namespace WebApp.ServiceManager
{
    public class CommentsServiceManager : ICommentsServiceManager
    {
        private readonly ICommentsBLLManager _commentsBLL;
        public CommentsServiceManager(ICommentsBLLManager commentsBLL)
        {
            _commentsBLL = commentsBLL;
        }

        public async Task<ResponseMessage> DeleteNewsCommentsById(long commentsId)
        {
            return await _commentsBLL.DeleteNewsCommentsById(commentsId);
        }

        public async Task<ResponseMessage> GetAllComments()
        {
            return await _commentsBLL.GetAllComments();
        }

        public async Task<ResponseMessage> GetNewsCommentsById(long commentsId)
        {
            return await _commentsBLL.GetNewsCommentsById(commentsId);
        }
    }
}
