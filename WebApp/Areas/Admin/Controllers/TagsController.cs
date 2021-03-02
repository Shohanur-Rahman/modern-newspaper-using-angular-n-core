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
    public class TagsController : Controller
    {
        private readonly INewsServiceManager _newsService;
        public TagsController(INewsServiceManager newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _newsService.GetAllNewsTags();
            IList<VMNewsChildModel> tags = response.data as List<VMNewsChildModel>;
            tags = (tags == null) ? new List<VMNewsChildModel>() : tags;
            return View(tags);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _newsService.GetNewsTagsById(id);
                var data = (VMNewsChildModel)response.data;
                return View(data);
            }
            catch(Exception ex)
            {
                string error = ex.Message.ToString();
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VMNewsChildModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _newsService.SaveUpdateNewsTags(model);
                if (response.isSuccess == true)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_CREATED);
                    return RedirectToAction("", "tags", new { area = "admin" });
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
                var response = await _newsService.SaveUpdateNewsTags(model);
                if (response.isSuccess == true)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_UPDATED);
                    return RedirectToAction("", "tags", new { area = "admin" });
                }

                return View();
            }
            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {
            var response = await _newsService.DeleteNewsTags(id);
            if (response.isSuccess == true)
            {
                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_DELETED);
                return RedirectToAction("", "tags", new { area = "admin" });
            }
            return View();
        }
    }
}
