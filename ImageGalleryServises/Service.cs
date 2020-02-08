using ImageGallary.Data;
using ImageGallary.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageGalleryServises
{
    public class Service : ImageGallary.Data.IImage
    {
        private ImageGalleryDbContext ImageGalleryDbContext;
        public Service(ImageGalleryDbContext imageGalleryDb) 
        {
            ImageGalleryDbContext = imageGalleryDb;
        }
        IEnumerable<GalleryImage> IImage.GetAll()=>ImageGalleryDbContext.GalleryImages.Include(images => images.Tags);

        GalleryImage IImage.GetById(int id)
        {
            return ImageGalleryDbContext.GalleryImages.Include(images => images.Tags).Where(Image => Image.Id == id).First();
        }
        IEnumerable<GalleryImage> IImage.GetByTag(string tag)=> ImageGalleryDbContext.GalleryImages.Include(images => images.Tags).Where(Image => Image.Tags.Any(tg => tg.Description == tag));
    }
}
