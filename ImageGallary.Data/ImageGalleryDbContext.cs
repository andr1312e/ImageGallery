using Microsoft.EntityFrameworkCore;
using System;

namespace ImageGallary.Data
{
    public class ImageGalleryDbContext:DbContext
    {
        public ImageGalleryDbContext(DbContextOptions options) : base(options) 
        {
        }
        public DbSet<GalleryImage> GalleryImages { set; get; }
    }
}
