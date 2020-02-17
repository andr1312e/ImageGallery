using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGalleryUsers.CustomIdentityApp.Models;
using ImageGalleryUsers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        public AdminController(UserManager<AppUser> user)
        {
            userManager = user;
        }
        public ViewResult Index()
        {
            return View(userManager.Users);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult identityResult = await userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError identityError in identityResult.Errors)
                    {
                        ModelState.AddModelError(identityError.Code +" ", identityError.Description);
                    }
                }
            }
            return View(model);
        }
    }
}