using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ImageGallary.Data;
using ImageGallery.Models;
using ImageGalleryServises;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ImageGallery.Models;
using Microsoft.Extensions.Logging;

namespace ImageGallery.Controllers
{
    public class ImageController : Controller
    {
        public IImage service;
        IWebHostEnvironment _appEnvironment;
        public ImageController(IImage _service, IWebHostEnvironment appEnvironment/*, IFormFile uploadedFile*/)
        {
            service = _service;
            _appEnvironment = appEnvironment;
        }
        public IActionResult Upload(/*[FromForm] string title, [FromForm] string Tags, [FromForm] IFormFile uploadedFile*/)
        {//////CHECK
          //  var model = new UploadModel()
          //  {
          //      Title = title,
          //      Tags = Tags,
          //      uploadedFile = uploadedFile
          //};
          return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile([FromForm] string title, [FromForm] string Tags, IFormFile uploadedFile)
        {
            if (title == null)
            {
                title = "No name";
            }
            if (Tags == null)
            {
                Tags = "";
            }
            string user = HttpContext.User.Identity.Name;
            string type = uploadedFile.ContentType.Substring(0, uploadedFile.ContentType.IndexOf('/'));
            if (uploadedFile != null&&type=="image")
            {
                string path = "/gallery/" + (service.LastId()+1).ToString()+"."+uploadedFile.ContentType.Substring((uploadedFile.ContentType.IndexOf('/')+1), uploadedFile.ContentType.Length- uploadedFile.ContentType.IndexOf('/')-1);
                var FileName = uploadedFile.FileName.Trim('"');
                using (var fileStream = new System.IO.FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                service.SetImage(title, Tags, path, user);
            }
            return RedirectToAction("Index", "ImageGallery");
        }
        public async Task<IActionResult> Delete(int id) 
        {
            var path = service.GetById(id).Url;
            System.IO.File.Delete(_appEnvironment.WebRootPath + path);
            service.Delete(service.GetById(id));
            return RedirectToAction("Index", "ImageGallery");
        }

        public async Task<IActionResult> UpdateImage(int id)
        {
            var model=service.GetById(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] string title, IFormFile uploadedFile)
        {
            var model = service.GetById(id);
            if (title != null && title!=model.Title)
            {
                service.Rename(model, title);
            }

            if (uploadedFile != null)
            {
                string type = uploadedFile.ContentType.Substring(0, uploadedFile.ContentType.IndexOf('/'));
                if (type == "image")
                {
                    var oldpath = service.GetById(id).Url;
                    var FileName = uploadedFile.FileName.Trim('"');
                    System.IO.File.Delete(_appEnvironment.WebRootPath + oldpath);
                    string path = "/gallery/" + (id + 1).ToString() + "." + uploadedFile.ContentType.Substring((uploadedFile.ContentType.IndexOf('/') + 1), uploadedFile.ContentType.Length - uploadedFile.ContentType.IndexOf('/') - 1);
                    using (var fileStream = new System.IO.FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    service.EditImage(model, path);
                }
            }
            return RedirectToAction("Index", "ImageGallery");
        }
    }  
}