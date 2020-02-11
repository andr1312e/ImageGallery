using ImageGallary.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace ImageGallary.Data
{
    public class ImageGalleryDbContext:DbContext
    {
        public ImageGalleryDbContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<GalleryImage> GalleryImages { set; get; }
        public DbSet<Tag> ImageTags { set; get;  }
    }
}
