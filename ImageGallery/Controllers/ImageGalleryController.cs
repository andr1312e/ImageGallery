using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageGallery.Models;
using ImageGallary.Data.Model;
using ImageGallary.Data.Data;
using ImageGallary.Data;
using Microsoft.AspNetCore.Identity;
using CustomIdentityApp.Models;

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
                SearchQuery=""

            };
            return View(model);
        }
        [Route("{id}/[controller]/[action]", Name = "Detail")]
        public IActionResult Detail(int id)
        {
            var user = HttpContext.User;
            var image = _ImageService.GetById(id);
            string userName = "Аноним";
            if (user != null)
            {
                if (user.Identity.Name == image.UserName)
                    userName = user.Identity.Name;
            }
            var model = new GalleryOnePictureViewModel()
            {
                Id = id,
                DateTimeCreated = image.ImageCreated,
                Title = image.Title,
                Url = image.Url,
                UserName= userName,
                Tags =image.Tags.Select(tags=>tags.Description).ToList()
            };
            return View(model); 
        }
        public IActionResult CurrentUserImages(string UserName)
        {
            GalleryModel model = new GalleryModel()
            {
                Images = _ImageService.GetByUserName(UserName),
                SearchQuery = ""

            };
            return View("Index",model);
        }
    }
}