using App.Models.VMModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApp.Constants;
using WebApp.Helper;
using WebApp.IServiceManager;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserProfileService _profileService;
        public ProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }
        // GET: ProfileController
        public async Task<ActionResult> Index()
        {
            //Get the current claims principal

            var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            Int64.TryParse(claimsId, out long userId);
            var profileResponse = await _profileService.GetUserProfileData(userId);

            var userProfile = (VMUserProfileModel)profileResponse.data;
            userProfile.ProfileDescription = HttpUtility.HtmlDecode(userProfile.ProfileDescription);

            return View(userProfile);
        }


        [ActionName("change-password")]
        public ActionResult ChangePassword()
        {
            return View("~/Areas/Admin/Views/Profile/ChangePassword.cshtml");
        }

        [ActionName("activities")]
        public ActionResult Activities()
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMUserProfileModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
                    Int64.TryParse(claimsId, out long userId);
                    collection.UserId = userId;
                    var response = await _profileService.SaveUpdateUserProfile(collection);
                    if (response.isSuccess)
                    {
                        DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.PROFILE_UPDATED);
                        return RedirectToAction("", "profile", new { area = "admin" });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("change-password")]
        public async Task<ActionResult> ChangePassword(VMChangePasswordModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string currentEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
                    var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
                    Int64.TryParse(claimsId, out long userId);
                    if (currentEmail.ToLower() != collection.Email.ToLower())
                    {
                        DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ConstantUserMessages.EMAIL_INVALID);
                        return RedirectToAction("change-password", "profile", new { area = "admin" });
                    }

                    collection.Id = userId;

                    var response = await _profileService.ChangePassword(collection);

                    if(response.isSuccess == true)
                    {
                        DisplayMessageHelper.ErrorMessageSetOrGet(this, true, ConstantUserMessages.PASSWORD_CHANGED);
                        return RedirectToAction("", "dashboard", new { area = "admin" });
                    }
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
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
