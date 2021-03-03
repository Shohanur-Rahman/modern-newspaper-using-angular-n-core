using App.Models.VMModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Constants;
using WebApp.Helper;
using WebApp.IServiceManager;

namespace WebApp.Controllers
{
    [Route("accounts")]
    public class AccountsController : Controller
    {
        private readonly IUserServiceManager _userServiceManager;
        public AccountsController(IUserServiceManager serviceManager)
        {
            _userServiceManager = serviceManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(VMUserModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userServiceManager.SaveUpdateUser(model);

                if (response.isSuccess == true)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.REGISTRATION_SUCCESS);
                    return RedirectToAction("login", "accounts");
                }

                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, response.message);
                return RedirectToAction("register", "accounts");
            }
            return View("~/Views/Accounts/Register.cshtml");
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(VMLoginModel model)
        {
            if (ModelState.IsValid)
            {

                var response = _userServiceManager.Login(model);
                if (response.isSuccess == true)
                {
                    var userObject = (VMLoginModel)(response.data);

                    var clims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, userObject.Id.ToString()),
                        new Claim(ClaimTypes.Name, userObject.Name),
                        new Claim(ClaimTypes.Email, userObject.Email),
                        new Claim(ClaimTypes.Role, (userObject.Role == null)? "User": userObject.Role.ToString())
                    };


                    var claimsIdentity = new ClaimsIdentity(clims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // Remember me
                        IsPersistent = model.RememberMe,

                        //Till
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)

                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);


                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.REGISTRATION_SUCCESS);
                    return RedirectToAction("", "dashboard", new { area = "admin" });
                }

                DisplayMessageHelper.ErrorMessageSetOrGet(this, true, response.message);
                return RedirectToAction("login", "accounts");
            }

            return View("~/Views/Accounts/Login.cshtml");
        }


        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("", "");
        }
    }
}
