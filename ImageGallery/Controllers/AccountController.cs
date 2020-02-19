﻿using System;
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
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            return View(details);
        }
    }
}