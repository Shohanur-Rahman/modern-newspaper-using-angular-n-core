using App.BLL.IBLLManager;
using App.Common.Constant;
using App.Common.Enums;
using App.Common.Response;
using App.Common.Static;
using App.Models.AppContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using App.Models.VMModels;
using Microsoft.EntityFrameworkCore;
using App.Models.Models;

namespace App.BLL.BLLManager
{
    public class FrontNewsBLLManager : IFrontNewsBLLManager
    {
        private readonly ApplicationDbContext _context;
        public FrontNewsBLLManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseMessage> GetBreakingNews()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await (from news in _context.NewsPost
                                        where news.IsBreaking == true
                                        select new VMBreakingNews()
                                        {
                                            Id = news.Id,
                                            Title = news.Title,
                                            CategorySlug = (from sl in _context.NewsCategories where sl.Id == news.CategoryId select sl.Slug).FirstOrDefault(),
                                            TitleSlug = news.TitleSlug
                                        }).ToListAsync();

                if (listOfNews == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                return result = ResponseMapping.GetResponseMessage(listOfNews, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetNewsDetailsByCategoryAndTitle(string catSlug, string titleSlug)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {

                var categoryId = await _context.NewsCategories.Where(c => c.Slug == catSlug).Select(c => c.Id).FirstOrDefaultAsync();
                var newsInformation = await (from news in _context.NewsPost
                                             where news.CategoryId == categoryId && news.TitleSlug == titleSlug
                                             select new VMNewsDetailsModel()
                                             {
                                                 Id = news.Id,
                                                 Title = news.Title,
                                                 ShortDescription = news.ShortDescription,
                                                 Description = news.Description,
                                                 CreatedId = news.CreatedId,
                                                 CreatedDate = news.CreatedDate,
                                                 FeaturedImage = news.FeaturedImage,
                                             }).FirstOrDefaultAsync();

                if (newsInformation == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                var listOfTag = await (from listOfTagMapper in _context.NewsTagMapper
                                       join tags in _context.NewsTags on listOfTagMapper.TagId equals tags.Id
                                       where listOfTagMapper.NewsId == newsInformation.Id
                                       select tags).AsQueryable().ToListAsync();

                var listOfCategories = await (from listOfCatMapper in _context.NewsCategoryMapper
                                              join cat in _context.NewsCategories on listOfCatMapper.CategoryId equals cat.Id
                                              where listOfCatMapper.NewsId == newsInformation.Id
                                              select cat).AsQueryable().ToListAsync();

                var reporterProfile = await (from profile in _context.UserProfile
                                             where profile.UserId == newsInformation.CreatedId
                                             select new UserLinkedProfile()
                                             {
                                                 ProfileId = profile.UserId,
                                                 ProfilePicture = profile.ProfilePicture,
                                                 UserBio = profile.UserBio,
                                                 DisplayName = profile.DisplayName,
                                             }).AsQueryable().FirstOrDefaultAsync();

                var listOfRelatedNews = await (from relatedNews in _context.NewsPost
                                               where relatedNews.CategoryId == categoryId
                                               && relatedNews.Id != newsInformation.Id
                                               select new VMNewsFrontModel()
                                               {
                                                   Id = relatedNews.Id,
                                                   Title = relatedNews.Title,
                                                   CategoryInfo = (from catInfo in _context.NewsCategories
                                                                   where catInfo.Id == categoryId
                                                                   select new VMNewsCategory()
                                                                   {
                                                                       Category = catInfo.CategoryName,
                                                                       CategorySlug = catInfo.Slug
                                                                   }).FirstOrDefault(),
                                                   TitleSlug = relatedNews.TitleSlug,
                                                   FeaturedImage = relatedNews.FeaturedImage,
                                                   CreatedDate = relatedNews.CreatedDate
                                               }).Take(3).AsQueryable().ToListAsync();

                var listOfComments = await (from comments in _context.NewsComment
                                            where comments.NewsId == newsInformation.Id && comments.ReplyId == null
                                            && comments.IsApprove == true
                                            select new VMNewsComments()
                                            {
                                                Id = comments.Id,
                                                CommentsDate = comments.CreatedDate,
                                                Commentor = (from commentor in _context.UserProfile
                                                             where commentor.UserId == comments.UserId
                                                             select new UserLinkedProfile()
                                                             {
                                                                 ProfileId = commentor.UserId,
                                                                 ProfilePicture = commentor.ProfilePicture,
                                                                 UserBio = commentor.UserBio,
                                                                 DisplayName = commentor.DisplayName,
                                                             }).AsQueryable().FirstOrDefault(),
                                                Comments = comments.Comments,
                                                ListOfReply = (from replies in _context.NewsComment
                                                               where replies.ReplyId == comments.Id
                                                               select new VMNewsComments()
                                                               {
                                                                   Id = replies.Id,
                                                                   CommentsDate = replies.CreatedDate,
                                                                   Commentor = (from commentor in _context.UserProfile
                                                                                where commentor.UserId == replies.UserId
                                                                                select new UserLinkedProfile()
                                                                                {
                                                                                    ProfileId = commentor.UserId,
                                                                                    ProfilePicture = commentor.ProfilePicture,
                                                                                    UserBio = commentor.UserBio,
                                                                                    DisplayName = commentor.DisplayName,
                                                                                }).AsQueryable().FirstOrDefault(),
                                                                   Comments = replies.Comments,
                                                               }).AsQueryable().ToList()

                                            }).AsQueryable().ToListAsync();

                newsInformation.ListOfCategories = listOfCategories;
                newsInformation.ListOfComments = listOfComments;
                newsInformation.RelatedNews = listOfRelatedNews;
                newsInformation.ReporterProfile = reporterProfile;
                newsInformation.ListOfTag = listOfTag;

                return result = ResponseMapping.GetResponseMessage(newsInformation, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetRelatedNewsByCategory(long categoryId)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await (from news in _context.NewsPost
                                        where news.IsBreaking == true
                                        select new VMNewsFrontModel()
                                        {
                                            Id = news.Id,
                                            Title = news.Title,
                                            CategoryInfo = (from catInfo in _context.NewsCategories
                                                            where catInfo.Id == news.CategoryId
                                                            select new VMNewsCategory()
                                                            {
                                                                Category = catInfo.CategoryName,
                                                                CategorySlug = catInfo.Slug
                                                            }).FirstOrDefault(),
                                            TitleSlug = news.TitleSlug,
                                            FeaturedImage = news.FeaturedImage
                                        }).ToListAsync();

                if (listOfNews == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                return result = ResponseMapping.GetResponseMessage(listOfNews, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> SaveNewsComment(VMNewsComments model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {

                var comment = MapNewsComments(new NewsComments(), model);

                _context.NewsComment.Add(comment);
                await _context.SaveChangesAsync();

                return result = ResponseMapping.GetResponseMessage(comment, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }


        public async Task<ResponseMessage> UpdateNewsComment(VMNewsComments model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {

                var comment = await _context.NewsComment.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                if(comment != null)
                {
                    comment = MapNewsComments(comment,model);
                    _context.Entry(comment).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return result = ResponseMapping.GetResponseMessage(comment, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);

                }
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.DataNotFound);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public NewsComments MapNewsComments(NewsComments comment, VMNewsComments model)
        {
            comment.Id = model.Id;
            comment.NewsId = (model.NewsId == null) ? comment.NewsId : model.NewsId;
            comment.ReplyId = (model.ReplyId == null) ? comment.ReplyId : model.ReplyId;
            comment.UserId = model.UserId;
            comment.Comments = (!string.IsNullOrEmpty(model.Comments)) ? model.Comments : comment.Comments;
            comment.CreatedDate = (model.CommentsDate == null) ? comment.CreatedDate : model.CommentsDate;
            comment.EditedDate= (model.EditedDate == null) ? comment.EditedDate : model.EditedDate;
            comment.EditedId = (model.EditedId == null) ? comment.EditedId : model.EditedId;
            comment.IsApprove = model.IsApprove;

            return comment;
        }

        public async Task<ResponseMessage> RecentThreeNews()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await (from news in _context.NewsPost
                                        orderby news.Id descending
                                        select new VMNewsFrontModel()
                                        {
                                            Id = news.Id,
                                            Title = news.Title,
                                            CategorySlug = (from sl in _context.NewsCategories where sl.Id == news.CategoryId select sl.Slug).FirstOrDefault(),
                                            TitleSlug = news.TitleSlug,
                                            FeaturedImage = news.FeaturedImage,
                                            CreatedDate = news.CreatedDate,
                                            DateDifference = (DateTime.Now - news.CreatedDate.Value).TotalMinutes.ToString()

                                        }).Take(18).ToListAsync();

                if (listOfNews == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                return result = ResponseMapping.GetResponseMessage(listOfNews, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

    }
}
