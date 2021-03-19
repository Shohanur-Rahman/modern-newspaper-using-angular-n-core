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
    public class ProfileController : Controller
    {

        private readonly IUserProfileService _profileService;
        public ProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        [Route("profile/{id}")]
        public async Task<IActionResult> Index(long id)
        {
            var profileResponse = await _profileService.GetUserProfileData(id);

            var userProfile = (VMUserProfileModel)profileResponse.data;
            userProfile.ProfileDescription = HttpUtility.HtmlDecode(userProfile.ProfileDescription);

            return View(userProfile);
        }
    }
}
