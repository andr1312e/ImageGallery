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
using Microsoft.Extensions.Configuration;

namespace ImageGallery.Controllers
{
    public class ImageController : Controller
    {
        public IImage service;
        IWebHostEnvironment _appEnvironment;
        UploadModel a = new UploadModel();
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
        [ValidateAntiForgeryToken]
        [HttpPost()]
        public async Task<IActionResult> OnPost([FromForm] string title, [FromForm] string Tags, [FromForm(Name = "file")] IFormFile uploadedFile)
        {

       //     System.Net.FtpWebRequest request =
       //(FtpWebRequest)WebRequest.Create("ftp://ftp.example.com/remote/path/file.zip");
       //     request.Credentials = new NetworkCredential("username", "password");
       //     request.Method = WebRequestMethods.Ftp.UploadFile;

       //     using (Stream ftpStream = request.GetRequestStream())
       //     {
       //         file.CopyTo(ftpStream);
       //     }


            var content = ContentDispositionHeaderValue.Parse(uploadedFile.ContentDisposition);

            if (uploadedFile != null)
            {
                // путь к папке 
                string path = "\\gallery\\" + uploadedFile.FileName;
                var FileName = uploadedFile.FileName.Trim('"');
                // сохраняем файл в папку  в каталоге wwwroot
                using (var fileStream = new System.IO.FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                service.SetImage(title, Tags, _appEnvironment.WebRootPath+ path);
                service.SaveChanges();
            }
            return RedirectToAction("Index", "Gallery");
        }
    }  
}