using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageGallery.Models;
using ImageGallary.Data.Model;
using ImageGallary.Data.Data;
using ImageGallary.Data;

namespace ImageGallery.Controllers
{
    public class ImageGalleryController : Controller
    {
        private readonly IImage _ImageService;
        public ImageGalleryController(IImage service) => _ImageService = service;
        public IActionResult Index()
        {
            var ImageList = _ImageService.GetAll();
           // var ImageList = new List<GalleryImage>()
            //{
            //    new GalleryImage()
            //    {
            //        Title="Dennis",
            //        Url="https://bipbap.ru/wp-content/uploads/2017/10/0_8eb56_842bba74_XL-640x400.jpg",
            //        ImageCreated=DateTime.Now,
            //        Tags=new List<Tag>()
            //        {
            //            Collection_of_Tags.Adventure,
            //            Collection_of_Tags.GroupMates
            //        }
                    
            //    },
            //    new GalleryImage()
            //    {
            //        Title="Artemka",
            //        Url="https://bipbap.ru/wp-content/uploads/2017/10/0_8eb56_842bba74_XL-640x400.jpg",
            //        ImageCreated=DateTime.MinValue,
            //        Tags=new List<Tag> ()
            //        {
            //            Collection_of_Tags.GroupMates
            //        }
            //    },
            //    new GalleryImage()
            //    {
            //        Title="Пыльца",
            //        Url="https://bipbap.ru/wp-content/uploads/2017/10/0_8eb56_842bba74_XL-640x400.jpg",
            //        ImageCreated=DateTime.MinValue,
            //        Tags=new List<Tag> ()
            //        {
            //            Collection_of_Tags.GroupMates,
            //            Collection_of_Tags.Adventure,
            //            Collection_of_Tags.Me
            //        }
            //    },
            //    new GalleryImage()
            //    {
            //        Title="Me",
            //        Url="https://bipbap.ru/wp-content/uploads/2017/10/0_8eb56_842bba74_XL-640x400.jpg",
            //        ImageCreated=DateTime.MinValue,
            //        Tags=new List<Tag> ()
            //        {
            //            Collection_of_Tags.Me,
            //            Collection_of_Tags.Friends
            //        }
            //    }
            //};
            GalleryModel model = new GalleryModel()
            {
                Images = ImageList,
                SearchQuery=""

            };
            return View(model);
        }
    }
}