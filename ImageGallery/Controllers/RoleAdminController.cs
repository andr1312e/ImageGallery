using CustomIdentityApp.Models;
using ImageGallary.Data;
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
        private UserManager<AppUser> userManager;
        public RoleAdminController(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
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

        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole identityRole = await roleManager.FindByIdAsync(id);
            if (identityRole != null)
            {
                List<AppUser> members = new List<AppUser>();
                List<AppUser> nonmembers = new List<AppUser>();
                if (userManager.Users.Count() > 0)
                {
                    foreach (var user in userManager.Users)
                    {
                        var list = await userManager.IsInRoleAsync(user, identityRole.Name) ? members : nonmembers;
                        list.Add(user);
                    }
                }
                return View(new ImageGallary.Data.RoleEditModel
                {
                    Role = identityRole,
                    Members = members,
                    NonMembers = nonmembers
                });
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach (string userId in model.IdsToDelte ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return await Edit(model.RoleId);
            }
        }
    }
}
