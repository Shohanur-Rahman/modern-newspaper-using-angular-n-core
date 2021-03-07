using App.Models.VMModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Constants;
using WebApp.Helper;
using WebApp.IServiceManager;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentsServiceManager _commentService;
        private readonly INewsFrontService _newsFrontService;
        public CommentsController(ICommentsServiceManager commentsService, INewsFrontService newsFrontService)
        {
            _commentService = commentsService;
            _newsFrontService = newsFrontService;
        }
        // GET: CommentsController
        public async Task<ActionResult> Index()
        {
            var commentsList = await _commentService.GetAllComments();
            IList<VMNewsComments> listOfComments = commentsList.data as List<VMNewsComments>;
            return View(listOfComments);
        }

        // GET: CommentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMNewsComments collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
                    Int64.TryParse(claimsId, out long userId);
                    collection.UserId = userId;
                    collection.CommentsDate = DateTime.Now;
                    collection.CreatedId = userId;

                    var responseResult = await _newsFrontService.SaveUpdateNewsComment(collection);

                    if (responseResult.isSuccess)
                    {
                        DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_CREATED);
                        return RedirectToAction("", "comments", new { area = "admin" });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentsController/Edit/5
        public async Task<ActionResult> Edit(long id)
        {
            var response = await _commentService.GetNewsCommentsById(id);
            VMNewsComments comments = (VMNewsComments)response.data;
            return View(comments);
        }

        // POST: CommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VMNewsComments collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
                    Int64.TryParse(claimsId, out long userId);
                    collection.UserId = userId;
                    collection.CommentsDate = DateTime.Now;
                    collection.CreatedId = userId;

                    var responseResult = await _newsFrontService.SaveUpdateNewsComment(collection);

                    if (responseResult.isSuccess)
                    {
                        DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_UPDATED);
                        return RedirectToAction("", "comments", new { area = "admin" });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentsController/Delete/5
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var responseResult = await _commentService.DeleteNewsCommentsById(id);

                if (responseResult.isSuccess)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_DELETED);
                    return RedirectToAction("", "comments", new { area = "admin" });
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: CommentsController/Delete/5
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
