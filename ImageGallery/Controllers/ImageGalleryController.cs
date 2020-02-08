using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageGallery.Models;
using ImageGallary.Data.Model;
using ImageGallary.Data.Data;
using ImageGallary.Data;

namespace ImageGallery.Controllers
{
    public class ImageGalleryController : Controller
    {
        private readonly IImage _ImageService;
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
            var image = _ImageService.GetById(id);
            var model = new GalleryOnePictureViewModel()
            {
                Id = id,
                DateTimeCreated = image.ImageCreated,
                Title = image.Title,
                Url = image.Url,
                Tags=image.Tags.Select(tags=>tags.Description).ToList()
            };
            return View(model); 
        }
    }
}