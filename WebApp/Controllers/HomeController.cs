using App.Models.VMModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IServiceManager;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsFrontService _newService;
        public HomeController(ILogger<HomeController> logger, INewsFrontService newsService)
        {
            _logger = logger;
            _newService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var breakingNewsResponse = await _newService.GetBreakingNews();
            IList<VMBreakingNews> breakingNews = breakingNewsResponse.data as IList<VMBreakingNews>;
            ViewBag.BreakingNews = breakingNews;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
