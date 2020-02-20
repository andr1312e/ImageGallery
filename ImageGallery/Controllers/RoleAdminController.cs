using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Controllers
{
    public class RoleAdminController:Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public RoleAdminController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }
        public ViewResult Index() => View(roleManager.Roles);
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create([Required] string Name)
        {
            if (ModelState.IsValid)
            {
                if(!await roleManager.RoleExistsAsync(Name))
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(Name));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            return View(Name);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError identityError in result.Errors)
            {
                ModelState.AddModelError("", identityError.Description);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult identityResult = await roleManager.DeleteAsync(role);
                if (identityResult.Succeeded)
                {
                    RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(identityResult);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", roleManager.Roles);
        }
    }
}
