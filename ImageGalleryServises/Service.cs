using ImageGallary.Data;
using ImageGallary.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageGalleryServises
{
    public class Service : IImage
    {
        private ImageGalleryDbContext ImageGalleryDbContext;
        public Service(ImageGalleryDbContext imageGalleryDb) 
        {
            ImageGalleryDbContext = imageGalleryDb;
        }
        IEnumerable<GalleryImage> IImage.GetAll()=>ImageGalleryDbContext.GalleryImages.Include(images => images.Tags);

        GalleryImage IImage.GetById(int id)
        {
            try
            {
                return ImageGalleryDbContext.GalleryImages.Include(images => images.Tags).Where(Image => Image.Id == id).First();
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
        IEnumerable<GalleryImage> IImage.GetByTag(string tag)=> ImageGalleryDbContext.GalleryImages.Include(images => images.Tags).Where(Image => Image.Tags.Any(tg => tg.Description == tag));

        IEnumerable<GalleryImage> IImage.GetByUserName(string CurrentUserName) => ImageGalleryDbContext.GalleryImages.Include(images => images.Tags).Where(Image=>Image.UserName==CurrentUserName);

        public  void SetImage(string title, string tags, string path, string user)
        {
            GalleryImage Image = new GalleryImage
            {
                Title = title,
                ImageCreated = DateTime.Now,
                UserName = user,
                Tags = TagsFromStringParse(tags),
                Url = path,
            };
            ImageGalleryDbContext.Add(Image);
            ImageGalleryDbContext.SaveChanges();
        }
        public void Delete(GalleryImage img) 
        {
            ImageGalleryDbContext.GalleryImages.Remove(img);
            ImageGalleryDbContext.SaveChanges();
        }
        public int LastId()
        {
            return (int)ImageGalleryDbContext.GalleryImages.Include(images => images.Tags).LongCount();
        }


        private IEnumerable<Tag> TagsFromStringParse(string tags)
        {
            var tagList = tags.Split(",").ToList().Select(tag => new Tag
            {
                Description = tag
            }).ToList();
            //var imageTags = new List<Tag>();
            //foreach (var tag in tagList)
            //{
            //    imageTags.Add(new Tag
            //    {
            //        Description = tag
            //    }) ;
            //}
            return tagList;
        }
    }
}
