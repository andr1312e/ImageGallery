using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class UploadModel
    {
        public string Title { set; get; }
        public string Tags { set; get; }
        public IFormFile IImageUpload { set; get; }
    }
}
