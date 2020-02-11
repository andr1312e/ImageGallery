using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ImageGallary.Data;
using ImageGallery.Models;
using ImageGalleryServises;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ImageGallery.Controllers
{
    public class ImageController : Controller
    {
        public IImage service;
        IWebHostEnvironment _appEnvironment;
        public ImageController(IImage _service, IWebHostEnvironment appEnvironment)
        {
            service = _service;
            _appEnvironment = appEnvironment;
        }
        public IActionResult Upload()
        {
            var model = new UploadModel()
            {

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadNewImage(IFormFile file, string title, string Tags)
        {
            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            if (file != null)
            {
                // путь к папке Files
                string path = "/gallery/" + file.FileName;
                var FileName = file.FileName.Trim('"');
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new System.IO.FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                service.SetImage(file.FileName, title, Tags, path);
                service.SaveChanges();
            }
            return RedirectToAction("Index", "Gallery");
        }
    }  
}