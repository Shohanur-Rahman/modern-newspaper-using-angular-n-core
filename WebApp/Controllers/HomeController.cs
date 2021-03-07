using App.Models.VMModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

            var recentThreeNewsResponse = await _newService.RecentThreeNews();
            IList<VMNewsFrontModel> recentThree = recentThreeNewsResponse.data as IList<VMNewsFrontModel>;
            ViewBag.RecentThree = recentThree.Take(3).ToList();

            ViewBag.TrendingNewsFirstThree = recentThree.Skip(3).Take(3).ToList();
            ViewBag.TrendingNewsSecondTwo = recentThree.Skip(6).Take(2).ToList();
            ViewBag.TrendingNewsThirdPart = recentThree.Skip(8).Take(7).ToList();

            ViewBag.TrendingNewsFourthPart = recentThree.Skip(15).Take(3).ToList();

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
