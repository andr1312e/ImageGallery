using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageGalleryUsers.Models;
using Microsoft.AspNetCore.Identity;
using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using ImageGallary.Data;

namespace ImageGallery.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private IUserValidator<AppUser> _userValidator;
        private IPasswordValidator<AppUser> _passwordValidator;
        private IPasswordHasher<AppUser> _passwordHasher;
        [Authorize]
        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "ImageGallery");
        }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserValidator<AppUser> userValidator, IPasswordValidator<AppUser> passwordValidator, IPasswordHasher<AppUser> passwordHasher)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Edit(string name)
        {
                AppUser user = await _userManager.FindByNameAsync(name);
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return RedirectToAction("ImageGallery","Index");
                }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string Email, string password)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = Email;
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
                        return RedirectToAction("Index", "ImageGallery");
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
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(LoginModel.Email), "Invalid email");
                }
            }
            return View(details);
        }
    }
}