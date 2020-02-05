using System.Collections.Generic;

namespace ImageGallery.Controllers
{
    internal class GalleryModel
    {
        public IEnumerable<Images> Images { get; set; }
        public string SearchQuery { set; get; }
    }
}