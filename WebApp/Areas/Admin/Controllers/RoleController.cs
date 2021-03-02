using App.Models.Models;
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
    public class RoleController : Controller
    {
        private readonly IUserRoleServiceManager _roleService;
        public RoleController(IUserRoleServiceManager roleService)
        {
            _roleService = roleService;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRoles();
            IList<UserRole> userRole = roles.data as List<UserRole>;
            userRole = (userRole == null) ? new List<UserRole>() : userRole;
            return View(userRole);
        }
        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetRoleById(id);
            return View((UserRole)role.data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRole model)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.SaveUpdateRole(model);

                if (response.isSuccess == true)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.ROLE_CREATED);
                    return RedirectToAction("", "role");
                }

                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, response.message);
                return RedirectToAction("create", "role");

            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRole model)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.SaveUpdateRole(model);

                if (response.isSuccess == true)
                {
                    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.ROLE_UPDATED);
                    return RedirectToAction("", "role", new { area = "admin" });
                }

                DisplayMessageHelper.SuccessMessageSetOrGet(this, true, response.message);
                return RedirectToAction("create", "role", new { area = "admin" });

            }
            return View();
        }
    }
}
