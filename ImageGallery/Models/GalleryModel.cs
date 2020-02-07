using ImageGallary.Data.Model;
using ImageGallery.Models;
using System.Collections.Generic;

namespace ImageGallery.Models
{
    public class GalleryModel
    {
        public IEnumerable<GalleryImage> Images { get; set; }
        public string SearchQuery { set; get; }
    }
}