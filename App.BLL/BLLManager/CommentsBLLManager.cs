using App.BLL.IBLLManager;
using App.Common.Constant;
using App.Common.Enums;
using App.Common.Response;
using App.Common.Static;
using App.Models.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using App.Models.VMModels;

namespace App.BLL.BLLManager
{
    public class CommentsBLLManager : ICommentsBLLManager
    {
        private readonly ApplicationDbContext _context;
        public CommentsBLLManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseMessage> DeleteNewsCommentsById(long commentsId)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var comment = await _context.NewsComment.Where(x => x.Id == commentsId).FirstOrDefaultAsync();

                if (comment == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                _context.NewsComment.Remove(comment);
                await _context.SaveChangesAsync();

                return result = ResponseMapping.GetResponseMessage(comment, (int)ResponseStatus.Success, ConstantMessaages.Deleted_Success);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetAllComments()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfComments = await (from comments in _context.NewsComment
                                            select new VMNewsComments()
                                            {
                                                Id = comments.Id,
                                                Comments = comments.Comments,
                                                Commentor = (from commentor in _context.UserProfile
                                                             where commentor.UserId == comments.UserId
                                                             select new UserLinkedProfile()
                                                             {
                                                                 ProfileId = commentor.UserId,
                                                                 ProfilePicture = commentor.ProfilePicture,
                                                                 UserBio = commentor.UserBio,
                                                                 DisplayName = commentor.DisplayName,
                                                             }).AsQueryable().FirstOrDefault(),
                                                IsApprove = comments.IsApprove,
                                                CommentsDate = comments.CreatedDate,

                                            }).ToListAsync();

                if (listOfComments == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                return result = ResponseMapping.GetResponseMessage(listOfComments, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetNewsCommentsById(long commentsId)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var comment = await (from comments in _context.NewsComment
                                     where comments.Id == commentsId
                                     select new VMNewsComments()
                                     {
                                         Id = comments.Id,
                                         Comments = comments.Comments,
                                         Commentor = (from commentor in _context.UserProfile
                                                      where commentor.UserId == comments.UserId
                                                      select new UserLinkedProfile()
                                                      {
                                                          ProfileId = commentor.UserId,
                                                          ProfilePicture = commentor.ProfilePicture,
                                                          UserBio = commentor.UserBio,
                                                          DisplayName = commentor.DisplayName,
                                                      }).AsQueryable().FirstOrDefault(),
                                         IsApprove = comments.IsApprove,
                                         CommentsDate = comments.CreatedDate,
                                         NewsId=comments.NewsId,
                                         UserId=comments.UserId

                                     }).FirstOrDefaultAsync();

                if (comment == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                return result = ResponseMapping.GetResponseMessage(comment, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }
    }
}
