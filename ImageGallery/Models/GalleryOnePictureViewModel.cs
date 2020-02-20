using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class GalleryOnePictureViewModel
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public List<string> Tags { set; get; }
        public DateTime DateTimeCreated { set; get; }
        public string Url { set; get; }
        public String UserName { set; get; }
    }
}
