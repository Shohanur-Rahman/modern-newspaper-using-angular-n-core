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
                                             let newsId = (from queryId in _context.NewsPost
                                                           where queryId.CategoryId == categoryId && queryId.TitleSlug == titleSlug
                                                           select queryId.Id).FirstOrDefault()
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
                                                 ListOfTag = (from listOfTagMapper in _context.NewsTagMapper
                                                              join tags in _context.NewsTags on listOfTagMapper.TagId equals tags.Id
                                                              where listOfTagMapper.NewsId == news.Id
                                                              select tags).AsQueryable().ToList(),

                                                 ListOfCategories = (from listOfCatMapper in _context.NewsCategoryMapper
                                                                     join cat in _context.NewsCategories on listOfCatMapper.CategoryId equals cat.Id
                                                                     where listOfCatMapper.NewsId == news.Id
                                                                     select cat).AsQueryable().ToList(),

                                                 ReporterProfile = (from profile in _context.UserProfile
                                                                    where profile.UserId == news.CreatedId
                                                                    select new UserLinkedProfile()
                                                                    {
                                                                        ProfileId = profile.UserId,
                                                                        ProfilePicture = profile.ProfilePicture,
                                                                        UserBio = profile.UserBio,
                                                                        DisplayName = profile.DisplayName,
                                                                    }).AsQueryable().FirstOrDefault(),
                                                 RelatedNews = (from relatedNews in _context.NewsPost
                                                                where relatedNews.CategoryId == categoryId
                                                                && relatedNews.Id != newsId
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
                                                                }).Take(3).AsQueryable().ToList(),
                                                 ListOfComments = (from comments in _context.NewsComment
                                                                   let commentId = (from cmt in _context.NewsComment
                                                                                    where cmt.NewsId == newsId
                                                                                    select cmt.Id).FirstOrDefault()
                                                                   where comments.NewsId == newsId
                                                                   select new VMNewsComments()
                                                                   {
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
                                                                                      where replies.ReplyId == commentId
                                                                                      select new VMNewsComments()
                                                                                      {
                                                                                          Commentor = (from commentor in _context.UserProfile
                                                                                                       where commentor.UserId == replies.UserId
                                                                                                       select new UserLinkedProfile()
                                                                                                       {
                                                                                                           ProfileId = commentor.UserId,
                                                                                                           ProfilePicture = commentor.ProfilePicture,
                                                                                                           UserBio = commentor.UserBio,
                                                                                                           DisplayName = commentor.DisplayName,
                                                                                                       }).AsQueryable().FirstOrDefault(),
                                                                                          Comments = comments.Comments,
                                                                                      }).AsQueryable().ToList()

                                                                   }).AsQueryable().ToList()

                                             }).FirstOrDefaultAsync();

                if (newsInformation == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

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
    }
}
