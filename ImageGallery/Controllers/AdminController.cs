using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentityApp.Models;
using ImageGalleryUsers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Authentication;

namespace ImageGallery.Controllers
{

    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IUserValidator<AppUser> _userValidator;
        private IPasswordValidator<AppUser> _passwordValidator;
        private IPasswordHasher<AppUser> _passwordHasher;
        private readonly SignInManager<AppUser> _signInManager;
        public AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserValidator<AppUser> userValidator, IPasswordHasher<AppUser> passwordHasher, IPasswordValidator<AppUser> passwordValidator)
        {
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _userValidator = userValidator;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        public IActionResult Create() => View();
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (EmailIsUnique(model.Email))
                {
                    AppUser user = new AppUser
                    {
                        UserName = model.Name,
                        Email = model.Email
                    };
                    IdentityResult identityResult = await _userManager.CreateAsync(user, model.Password);
                    if (identityResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else 
                    {
                        foreach (IdentityError identityError in identityResult.Errors)
                        {
                            ModelState.AddModelError(identityError.Code + " ", identityError.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email must be unique, try another email");
                }
            }
            else
            {
                ModelState.AddModelError("", "Model state is not valid");
            }
            return View(model);
        }

        private bool EmailIsUnique(string email)
        {
            var user = _userManager.FindByEmailAsync(email);
            if (_userManager.FindByEmailAsync(email).Result == null)
                return true;
            return false;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else 
            {
                ModelState.AddModelError("", "User not found");
            }
            return View("Index", _userManager.Users);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await _userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (password != null && password.Length >= 1)
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager, user, password);
                }
                if (validPass.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, password);
                }
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }
    }
}
