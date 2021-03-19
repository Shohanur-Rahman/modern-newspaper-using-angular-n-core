using App.BLL.IBLLManager;
using App.Common.Constant;
using App.Common.Enums;
using App.Common.Response;
using App.Common.Static;
using App.Models.AppContext;
using App.Models.Models;
using App.Models.VMModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.BLLManager
{
    public class NewsBLLManager : INewsBLLManager
    {
        private readonly ApplicationDbContext _context;
        public NewsBLLManager(ApplicationDbContext context)
        {
            _context = context;
        }

        #region"Delete Related Code"

        public async Task<ResponseMessage> DeleteNews(long id)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var news = await _context.NewsPost.Where(u => u.Id == id).FirstOrDefaultAsync();

                if (news == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                /*
                 * 
                 * 
                 * Delete list of tags mapper
                 * 
                 */


                var listOfTagMapper = await _context.NewsTagMapper.Where(t => t.NewsId == id).ToListAsync();

                foreach (var tagMapper in listOfTagMapper)
                {
                    _context.NewsTagMapper.Remove(tagMapper);
                    await _context.SaveChangesAsync();
                }

                /*
                 * 
                 * 
                 * Delete list of category mapper
                 * 
                 */

                var listOfCategory = await _context.NewsCategoryMapper.Where(c => c.NewsId == id).ToListAsync();

                foreach (var catMapper in listOfCategory)
                {
                    _context.NewsCategoryMapper.Remove(catMapper);
                    await _context.SaveChangesAsync();
                }


                /*
                 * 
                 * Finally delete the news
                 * 
                 */
                _context.NewsPost.Remove(news);
                await _context.SaveChangesAsync();

                return result = ResponseMapping.GetResponseMessage(news.Title, (int)ResponseStatus.Success, ConstantMessaages.Deleted_Success);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> DeleteNewsCategory(int id)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var category = await _context.NewsCategories.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (category == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                /*
                 * 
                 * 
                 * Delete list of category mapper
                 * 
                 */

                var listOfCategory = await _context.NewsCategoryMapper.Where(c => c.NewsId == id).ToListAsync();

                foreach (var catMapper in listOfCategory)
                {
                    _context.NewsCategoryMapper.Remove(catMapper);
                    await _context.SaveChangesAsync();
                }


                /*
                 * 
                 * Finally delete the news
                 * 
                 */
                _context.NewsCategories.Remove(category);
                await _context.SaveChangesAsync();

                return result = ResponseMapping.GetResponseMessage(category.CategoryName, (int)ResponseStatus.Success, ConstantMessaages.Deleted_Success);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> DeleteNewsTags(int id)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var tag = await _context.NewsTags.Where(u => u.Id == id).FirstOrDefaultAsync();

                if (tag == null)
                {
                    return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.FailRetrieve);
                }

                /*
                 * 
                 * 
                 * Delete list of tags mapper
                 * 
                 */


                var listOfTagMapper = await _context.NewsTagMapper.Where(t => t.NewsId == id).ToListAsync();

                foreach (var tagMapper in listOfTagMapper)
                {
                    _context.NewsTagMapper.Remove(tagMapper);
                    await _context.SaveChangesAsync();
                }


                /*
                 * 
                 * Finally delete the news
                 * 
                 */
                _context.NewsTags.Remove(tag);
                await _context.SaveChangesAsync();

                return result = ResponseMapping.GetResponseMessage(tag.Name, (int)ResponseStatus.Success, ConstantMessaages.Deleted_Success);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }
        #endregion


        #region"Get All List"



        public async Task<ResponseMessage> GetAllNews()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await (from news in _context.NewsPost
                                        select new VMNewsModel()
                                        {
                                            Id = news.Id,
                                            Title = news.Title,
                                            ShortDescription = news.ShortDescription,
                                            Description = news.Description,
                                            FeaturedImage = news.FeaturedImage,
                                            IsPublished = news.IsPublished,
                                            CreatedDate = news.CreatedDate
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

        public async Task<ResponseMessage> GetNewCattegoryTree()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await (from categories in _context.NewsCategories
                                        where categories.ParentId == null
                                        select new NewsCategoryTree()
                                        {
                                            CategoryName = categories.CategoryName,
                                            Id = categories.Id,
                                            ChildCategory = (from child in _context.NewsCategories
                                                             where categories.Id.Equals(child.ParentId.Value)
                                                             select new NewsCategoryTree()
                                                             {
                                                                 CategoryName = child.CategoryName,
                                                                 Id = child.Id,
                                                                 ChildCategory = (from subChild in _context.NewsCategories
                                                                                  where child.Id.Equals(subChild.ParentId.Value)
                                                                                  select new NewsCategoryTree()
                                                                                  {
                                                                                      CategoryName = subChild.CategoryName,
                                                                                      Id = subChild.Id,
                                                                                  }).AsQueryable().ToList(),
                                                             }).AsQueryable().ToList(),

                                        }).OrderBy(c => c.CategoryName).ToListAsync();


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

        public async Task<ResponseMessage> GetAllNewsCategories()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await (from categories in _context.NewsCategories
                                        select new VMNewsChildModel()
                                        {
                                            CategoryName = categories.CategoryName,
                                            Id = categories.Id,
                                            CreatedDate = categories.CreatedDate,
                                            TotalNews = (from news in _context.NewsPost
                                                         join map in _context.NewsCategoryMapper
                                                         on news.Id equals map.NewsId
                                                         where map.CategoryId == categories.Id
                                                         select map.Id).AsQueryable().Count(),
                                            Parent = (from p in _context.NewsCategories where p.Id == categories.ParentId select p.CategoryName).AsQueryable().FirstOrDefault()
                                        }).OrderBy(c => c.Parent).ToListAsync();


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

        public async Task<ResponseMessage> GetAllNewsTags()
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var listOfNews = await (from tags in _context.NewsTags
                                        select new VMNewsChildModel()
                                        {
                                            TagName = tags.Name,
                                            Id = tags.Id,
                                            CreatedDate = tags.CreatedDate,
                                            TotalNews = ((from news in _context.NewsTags
                                                          join map in _context.NewsTagMapper
                                                          on news.Id equals map.NewsId
                                                          where map.TagId == tags.Id
                                                          select map.Id).AsQueryable()).Count()
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



        #endregion


        #region"Get item by id"

        public async Task<ResponseMessage> GetNewsById(long id)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {

                var news = await (from n in _context.NewsPost
                                  let tags = (from tag in _context.NewsTagMapper
                                              where tag.NewsId == n.Id
                                              select tag.TagId.ToString()).AsQueryable().ToList()
                                  let categories = (from category in _context.NewsCategoryMapper
                                                    where category.NewsId == n.Id
                                                    select category.CategoryId.ToString()).AsQueryable().ToList()
                                  where n.Id == id
                                  select new VMNewsModel()
                                  {
                                      Title = n.Title,
                                      ShortDescription = n.ShortDescription,
                                      Description = n.Description,
                                      FeaturedImage = n.FeaturedImage,
                                      CategoryId = n.CategoryId,
                                      IsPublished = n.IsPublished,
                                      IsBreaking = n.IsBreaking,
                                      VideoURL=n.VideoURL,
                                      Tags = new[] { string.Join(",", tags) },
                                      Categories = new[] { string.Join(",", categories) }
                                  }).FirstOrDefaultAsync();


                if (news != null)
                {
                    return result = ResponseMapping.GetResponseMessage(news, (int)ResponseStatus.Fail, ConstantMessaages.RetrieveSuccess);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Success, ConstantMessaages.DataNotFound);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetNewsCategoryById(int id)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var category = await (from cat in _context.NewsCategories
                                      where cat.Id == id
                                      select new VMNewsChildModel
                                      {
                                          CategoryName = cat.CategoryName,
                                          Id = cat.Id,
                                          CreatedDate = cat.CreatedDate,
                                          IsPublished = cat.IsPublished
                                      }).FirstOrDefaultAsync();

                if (category != null)
                {
                    return result = ResponseMapping.GetResponseMessage(category, (int)ResponseStatus.Fail, ConstantMessaages.RetrieveSuccess);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Success, ConstantMessaages.DataNotFound);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> GetNewsTagsById(int id)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var tag = await (from cat in _context.NewsTags
                                 where cat.Id == id
                                 select new VMNewsChildModel
                                 {
                                     TagName = cat.Name,
                                     Id = cat.Id,
                                     CreatedDate = cat.CreatedDate
                                 }).FirstOrDefaultAsync();

                if (tag != null)
                {
                    return result = ResponseMapping.GetResponseMessage(tag, (int)ResponseStatus.Success, ConstantMessaages.RetrieveSuccess);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.DataNotFound);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }


        #endregion




        #region"Save Metods"

        public async Task<ResponseMessage> SaveNews(VMNewsModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {

                NewsPost news = MapNewsPost(new NewsPost(), model);
                _context.NewsPost.Add(news);
                await _context.SaveChangesAsync();

                bool status = await SaveOrUpdateNewsTagsAndCategoryMapper(news.Id, model);

                return result = ResponseMapping.GetResponseMessage(news, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);

            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> SaveNewsCategory(VMNewsChildModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var existingCategory = await _context.NewsCategories.Where(r => r.CategoryName.ToLower() == model.CategoryName.ToLower()).FirstOrDefaultAsync();

                if (existingCategory == null)
                {
                    NewsCategory category = new NewsCategory();
                    category.CategoryName = model.CategoryName;
                    category.IsPublished = model.IsPublished;
                    category.Slug = SlugGeneretor.GenerateSlug(model.CategoryName);
                    category.ParentId = model.ParentId;
                    category.CreatedId = model.CreatedId;
                    category.CreatedDate = DateTime.Now;

                    _context.NewsCategories.Add(category);
                    await _context.SaveChangesAsync();

                    return result = ResponseMapping.GetResponseMessage(category, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.AllReadyExist);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> SaveNewsTags(VMNewsChildModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var existingTag = await _context.NewsTags.Where(r => r.Name.ToLower() == model.TagName.ToLower()).FirstOrDefaultAsync();

                if (existingTag == null)
                {
                    NewsTags tag = new NewsTags();
                    tag.Name = model.TagName;
                    tag.CreatedId = model.CreatedId;
                    tag.CreatedDate = DateTime.Now;

                    _context.NewsTags.Add(tag);
                    await _context.SaveChangesAsync();

                    return result = ResponseMapping.GetResponseMessage(tag, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.AllReadyExist);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        #endregion


        #region"Update Methods"


        public async Task<ResponseMessage> UpdateNews(VMNewsModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {

                var news = await _context.NewsPost.Where(n => n.Id == model.Id).FirstOrDefaultAsync();

                if (news != null)
                {


                    news = MapNewsPost(news, model);
                    _context.Entry(news).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    bool status = await SaveOrUpdateNewsTagsAndCategoryMapper(news.Id, model);

                    return result = ResponseMapping.GetResponseMessage(news, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);

                }


                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.DataNotFound);
            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> UpdateNewsCategory(VMNewsChildModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var existingCategory = await _context.NewsCategories.Where(r => r.Id == model.Id).FirstOrDefaultAsync();

                if (existingCategory != null)
                {
                    existingCategory.CategoryName = model.CategoryName;
                    existingCategory.IsPublished = model.IsPublished;
                    existingCategory.ParentId = model.ParentId;
                    existingCategory.EditedId = model.EditedId;
                    existingCategory.EditedDate = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return result = ResponseMapping.GetResponseMessage(existingCategory, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.DataNotFound);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }

        public async Task<ResponseMessage> UpdateNewsTags(VMNewsChildModel model)
        {
            ResponseMessage result = new ResponseMessage();
            try
            {
                var existingTag = await _context.NewsTags.Where(r => r.Id == model.Id).FirstOrDefaultAsync();

                if (existingTag != null)
                {
                    existingTag.Name = model.TagName;
                    existingTag.EditedId = model.EditedId;
                    existingTag.EditedDate = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return result = ResponseMapping.GetResponseMessage(existingTag, (int)ResponseStatus.Success, ConstantMessaages.SuccessMessage);
                }

                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ConstantMessaages.AllReadyExist);


            }
            catch (Exception ex)
            {
                return result = ResponseMapping.GetResponseMessage(null, (int)ResponseStatus.Fail, ex.Message.ToString());
            }
        }
        #endregion


        private NewsPost MapNewsPost(NewsPost news, VMNewsModel model)
        {
            news.Id = model.Id;
            news.Title = (!string.IsNullOrEmpty(model.Title)) ? model.Title : news.Title;
            news.TitleSlug = (news.Id > 0) ? news.TitleSlug : SlugGeneretor.GenerateSlug(model.Title);
            news.IsPublished = model.IsPublished;
            news.ShortDescription = (!string.IsNullOrEmpty(model.ShortDescription)) ? model.ShortDescription : news.ShortDescription;
            news.Description = (!string.IsNullOrEmpty(model.Description)) ? model.Description : news.Description;
            news.CategoryId = model.CategoryId;
            news.IsBreaking = model.IsBreaking;
            news.VideoURL = model.VideoURL;
            news.FeaturedImage = (!string.IsNullOrEmpty(model.FeaturedImage)) ? model.FeaturedImage : news.FeaturedImage;
            news.CreatedDate = (model.CreatedDate != null) ? model.CreatedDate : news.CreatedDate;
            news.CreatedId = (model.CreatedId != null) ? model.CreatedId : news.CreatedId;
            news.EditedDate = (model.EditedDate != null) ? model.EditedDate : news.EditedDate;
            news.EditedId = (model.EditedId != null) ? model.EditedId : news.EditedId;

            return news;
        }

        private async Task<bool> SaveOrUpdateNewsTagsAndCategoryMapper(long newsId, VMNewsModel model)
        {
            bool isUpdated = false;
            try
            {
                /*
                     * 
                     * Save category mapper
                     * 
                     */

                var listOfCategories = await _context.NewsCategoryMapper.Where(n => n.NewsId == newsId).ToListAsync();

                foreach (var map in listOfCategories)
                {
                    _context.NewsCategoryMapper.Remove(map);
                    await _context.SaveChangesAsync();
                }

                //string[] categories = model.Categories.Split("#");
                foreach (var cat in model.Categories)
                {
                    if (string.IsNullOrEmpty(cat) == false && Int64.TryParse(cat, out long catId))
                    {
                        NewsCategoryMapper catMapper = new NewsCategoryMapper();
                        catMapper.NewsId = newsId;
                        catMapper.CategoryId = catId;
                        _context.NewsCategoryMapper.Add(catMapper);
                        await _context.SaveChangesAsync();
                    }
                }


                /*
                 * 
                 * Save Tag Mapper
                 * 
                 */


                var listOfTags = await _context.NewsTagMapper.Where(n => n.NewsId == newsId).ToListAsync();

                foreach (var map in listOfTags)
                {
                    _context.NewsTagMapper.Remove(map);
                    await _context.SaveChangesAsync();
                }

                //string[] tags = model.Tags.Split("#");
                foreach (var tag in model.Tags)
                {
                    if (string.IsNullOrEmpty(tag) == false && Int64.TryParse(tag, out long tagId))
                    {
                        NewsTagMapper tagMapper = new NewsTagMapper();
                        tagMapper.NewsId = newsId;
                        tagMapper.TagId = tagId;
                        _context.NewsTagMapper.Add(tagMapper);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                throw ex;
            }

            return isUpdated;
        }


    }
}
