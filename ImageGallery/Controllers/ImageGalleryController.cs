﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ImageGallery.Models;
using ImageGallary.Data.Model;
using ImageGallary.Data;

namespace ImageGallery.Controllers
{
    public class ImageGalleryController : Controller
    {
        private IImage _ImageService;
        public ImageGalleryController(IImage service) => _ImageService = service;
        public IActionResult Index()
        {
            var ImageList = _ImageService.GetAll();
            GalleryModel model = new GalleryModel()
            {
                Images = ImageList,
            };
            return View(model);
        }
        [Route("{id}/[controller]/[action]", Name = "Detail")]
        public IActionResult Detail(int id)
        {
            var user = HttpContext.User;
            var image = _ImageService.GetById(id);
            if (image != null)
            {
                string userName=_ImageService.SetImageAuthorInDetail(user.Identity.Name, image.UserName, user.IsInRole("Admin"));
                var model = new GalleryImage()
                {
                    Id = id,
                    ImageCreated = image.ImageCreated,
                    Title = image.Title,
                    Url = image.Url,
                    UserName = userName,
                    Tags = image.Tags
                };
                return View(model);
            }
            return RedirectToAction("Index", "ImageGallery");
        }
        public IActionResult CurrentUserImages(string UserName)
        {
            GalleryModel model = new GalleryModel()
            {
                Images = _ImageService.GetByUserName(UserName),
            };
            return View("Index",model);
        }
        public IActionResult CurrentTagImages(string tag)
        {
            GalleryModel model = new GalleryModel()
            {
                Images = _ImageService.GetByTag(tag),
            };
            return View("Index", model);
        }
    }
}