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
    public class SettingsController : Controller
    {
        private readonly INewsSettingsService _settingsService;
        private readonly INewsServiceManager _newsService;
        public SettingsController(INewsSettingsService settingsService, INewsServiceManager newsService)
        {
            _settingsService = settingsService;
            _newsService = newsService;

        }

        [Route("admin/settings")]
        [Route("admin/settings/home-categories")]
        public async Task<IActionResult> Index()
        {
            var categoryResponse = await _settingsService.GetNewsPaperSettingsRow();
            VMNewsPaperSettings settings = categoryResponse.data as VMNewsPaperSettings;
            ViewBag.Categories = (settings == null) ? new VMNewsPaperSettings() : settings;

            var response = await _newsService.GetAllNewsCategories();
            IList<VMNewsChildModel> categories = response.data as List<VMNewsChildModel>;
            ViewBag.Categories = (categories == null) ? new List<VMNewsChildModel>() : categories;

            return View(settings);
        }

        [HttpPost]
        [Route("admin/settings")]
        [Route("admin/settings/home-categories")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HomeCategorie(VMNewsPaperSettings model) {

            if (ModelState.IsValid)
            {
                var response = await _settingsService.SaveUpdateCategorySettings(model);
                if (response.isSuccess == true)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_UPDATED);
                    return RedirectToAction("", "settings", new { area = "admin" });
                }

                return View();
            }

            return View();
        }


    }
}
