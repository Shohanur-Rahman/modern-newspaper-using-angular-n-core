using App.Models.VMModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApp.Constants;
using WebApp.Helper;
using WebApp.IServiceManager;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsServiceManager _newsService;
        long userId;
        public NewsController(INewsServiceManager newsService)
        {
            _newsService = newsService;

        }

        // GET: NewsController
        public async Task<IActionResult> Index()
        {
            var response = await _newsService.GetAllNews();
            IList<VMNewsModel> news = response.data as List<VMNewsModel>;
            news = (news == null) ? new List<VMNewsModel>() : news;

            return View(news);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsController/Create
        public async Task<IActionResult> Create()
        {


            var categoryResponse = await _newsService.GetNewCattegoryTree();
            IList<NewsCategoryTree> categories = categoryResponse.data as List<NewsCategoryTree>;
            ViewBag.Categories = (categories == null) ? new List<NewsCategoryTree>() : categories;


            var tagsResponse = await _newsService.GetAllNewsTags();
            IList<VMNewsChildModel> tags = tagsResponse.data as List<VMNewsChildModel>;
            ViewBag.tags = (tags == null) ? new List<VMNewsChildModel>() : tags;

            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VMNewsModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
                    Int64.TryParse(claimsId, out userId);
                    collection.CreatedId = userId;
                    
                    var response = await _newsService.SaveUpdateNews(collection);

                    if (response.isSuccess == true)
                    {
                        DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_CREATED);
                        return RedirectToAction("", "news", new { area = "admin" });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsController/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var categoryResponse = await _newsService.GetNewCattegoryTree();
            IList<NewsCategoryTree> categories = categoryResponse.data as List<NewsCategoryTree>;
            ViewBag.Categories = (categories == null) ? new List<NewsCategoryTree>() : categories;


            var tagsResponse = await _newsService.GetAllNewsTags();
            IList<VMNewsChildModel> tags = tagsResponse.data as List<VMNewsChildModel>;
            ViewBag.tags = (tags == null) ? new List<VMNewsChildModel>() : tags;

            var newsResponse =  await _newsService.GetNewsById(id);

            var news = (VMNewsModel)newsResponse.data;

            news.Description = HttpUtility.HtmlDecode(news.Description);
            

            return View(news);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VMNewsModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
                    Int64.TryParse(claimsId, out userId);
                    collection.EditedId = userId;
                    var response = await _newsService.SaveUpdateNews(collection);

                    if (response.isSuccess == true)
                    {
                        DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_UPDATED);
                        return RedirectToAction("", "news", new { area = "admin" });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
