using App.Models.VMModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.IServiceManager;

namespace WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsFrontService _newService;
        public NewsController(INewsFrontService newsService)
        {
            _newService = newsService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Article(string category, string article)
        {
            if(article != "site.html")
            {
                var newsInfoResponse = await _newService.GetNewsDetailsByCategoryAndTitle(category, article);
                var newsInformation = (VMNewsDetailsModel)newsInfoResponse.data;

                newsInformation.Description = HttpUtility.HtmlDecode(newsInformation.Description);

                return View(newsInformation);
            }
            return View();

        }
    }
}
