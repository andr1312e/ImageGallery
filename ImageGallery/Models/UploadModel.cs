using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class UploadModel:PageModel
    {
        public string Title { set; get; }
        public string Tags { set; get; }
        public IFormFile uploadedFile { set; get; }
        public byte[] content { set; get; }
    }
}
