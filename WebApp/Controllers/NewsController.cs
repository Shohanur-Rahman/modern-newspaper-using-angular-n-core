using App.Models.VMModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Constants;
using WebApp.Helper;
using WebApp.IServiceManager;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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

        [HttpPost]
        [Route("news/post-comments")]
        public async Task<IActionResult> SubmitComment(VMNewsComments model)
        {
            if (ModelState.IsValid)
            {
                string path = model.CommentsPath;
                var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
                Int64.TryParse(claimsId, out long userId);
                model.UserId = userId;
                model.CommentsDate = DateTime.Now;
                var commentResponse = await _newService.SaveUpdateNewsComment(model);

                if (commentResponse.isSuccess)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.COMMENT_SUCCESS);

                    return Redirect(path);
                }
            }
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
            return View(new VMNewsDetailsModel());

        }

        
    }
}
