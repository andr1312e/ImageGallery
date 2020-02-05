using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Controllers
{
    public class ImageGalleryController : Controller
    {
        public IActionResult Index()
        {
            GalleryModel model = new GalleryModel();
            return View(model); 
        }
    }
}