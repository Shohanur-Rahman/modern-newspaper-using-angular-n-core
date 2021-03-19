using App.Models.VMModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IServiceManager;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly INewsSettingsService _settingsService;
        public SettingsController(INewsSettingsService settingsService)
        {
            _settingsService = settingsService;

        }

        [Route("admin/settings")]
        [Route("admin/settings/home-categories")]
        public async Task<IActionResult> Index()
        {
            var categoryResponse = await _settingsService.GetNewsPaperSettingsRow();
            VMNewsPaperSettings settings = categoryResponse.data as VMNewsPaperSettings;
            ViewBag.Categories = (settings == null) ? new VMNewsPaperSettings() : settings;

            return View(settings);
        }
    }
}
