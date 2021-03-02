using App.Models.VMModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Constants;
using WebApp.Helper;
using WebApp.IServiceManager;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly INewsServiceManager _newsService;
        public CategoriesController(INewsServiceManager newsService)
        {
            _newsService = newsService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _newsService.GetAllNewsCategories();
            IList<VMNewsChildModel> categories = response.data as List<VMNewsChildModel>;
            categories = (categories == null) ? new List<VMNewsChildModel>(): categories;
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            var response = await _newsService.GetAllNewsCategories();
            IList<VMNewsChildModel> categories = response.data as List<VMNewsChildModel>;
            ViewBag.Categories = (categories == null) ? new List<VMNewsChildModel>() : categories;
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _newsService.GetNewsCategoryById(id);

            var response = await _newsService.GetAllNewsCategories();
            IList<VMNewsChildModel> categories = response.data as List<VMNewsChildModel>;
            ViewBag.Categories = (categories == null) ? new List<VMNewsChildModel>() : categories;
            return View((VMNewsChildModel)category.data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VMNewsChildModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _newsService.SaveUpdateNewsCategory(model);
                if(response.isSuccess == true)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.CATEGORY_CREATED);
                    return RedirectToAction("", "categories", new { area = "admin" });
                }

                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VMNewsChildModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _newsService.SaveUpdateNewsCategory(model);
                if (response.isSuccess == true)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.CATEGORY_UPDATED);
                    return RedirectToAction("", "categories", new { area = "admin" });
                }

                return View();
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _newsService.DeleteNewsCategory(id);
            if (response.isSuccess == true)
            {
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_DELETED);
                return RedirectToAction("", "categories", new { area = "admin" });
            }
            return View();
        }
    }
}
