using System;
using System.Collections.Generic;

namespace ImageGallary.Data.Model
{
    public class GalleryImage
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public DateTime ImageCreated { set; get; }
        public string Url { set; get; }
        public IEnumerable<Tag> Tags { set; get; }
        public String UserName { set; get; }

    }
}