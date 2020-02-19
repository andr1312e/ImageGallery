using ImageGallary.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ImageGallary.Data
{
    public class ImageGalleryDbContext: DbContext
    {
        public ImageGalleryDbContext(DbContextOptions<ImageGalleryDbContext> options) : base(options) 
        {
        }
        public DbSet<GalleryImage> GalleryImages { set; get; }
        public DbSet<Tag> ImageTags { set; get;  }
    }
}
