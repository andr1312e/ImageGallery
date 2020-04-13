using ImageGallary.Data;
using ImageGallary.Data.Model;
using ImageGalleryServises;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
[TestFixture]  
    public class ServicesTests
    {

        [Category("Services")]
        [Test]
        public void Service_Edit_ImageUrl_Correct()
            {
                var options = new DbContextOptionsBuilder<ImageGalleryDbContext>()
                    .UseInMemoryDatabase(databaseName: "Service_Edit_ImageUrl_Correct").Options;

                using (var ctx = new ImageGalleryDbContext(options))
                {
                    ctx.GalleryImages.Add(new GalleryImage
                    {
                        Id = 19,
                        Title = "AAA",
                        Url = "1.JPEG"
                    });
                    ctx.SaveChanges();
                    IImage ImgService = new Service(ctx);
                    ImgService.EditImage(ImgService.GetById(19), "2.JPEG");
                    Assert.AreEqual("2.JPEG", ImgService.GetById(19).Url);
                }
            }
        [Category("Services")]
        [Test]
        public void Service_Return_All_Correct() 
        {
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase(databaseName: "Return_All_Images").Options;
            using var ctx = new ImageGalleryDbContext(options);
            ctx.GalleryImages.Add(new GalleryImage
            {
                Id=20,
                Title="1"
            });
            ctx.GalleryImages.Add(new GalleryImage
            {
                Id = 21,
                Title = "2"
            });
            ctx.SaveChanges();
            IImage service = new Service(ctx);
            int count=service.GetAll().Count();
            Assert.AreEqual(2, count);
        }
        [Category("Services")]
        [Test]
        public void Service_Get_By_Id_Correct() 
        {
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("Get_by_Id").Options;
            var ctx = new ImageGalleryDbContext(options);
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11"
            }) ;
            ctx.SaveChanges();
            IImage service = new Service(ctx);
            string TitleGet = service.GetById(1).Title;
            Assert.AreEqual("11", TitleGet);
        }
        [Category("Services")]
        [Test]
        public void Service_Rename_Image_Correct()
        {
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("Rename").Options;
            var ctx = new ImageGalleryDbContext(options);
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11"
            });
            ctx.SaveChanges();
            IImage service = new Service(ctx);
            service.Rename(service.GetById(1), "33");
            Assert.AreEqual("33", service.GetById(1).Title);
        }
        [Category("Services")]
        [Test]
        public void Service_Delete_Image_Correct()
        {
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("Delete").Options;
            var ctx = new ImageGalleryDbContext(options);
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11"
            });
            ctx.SaveChanges();
            IImage service = new Service(ctx);
            service.Delete(service.GetById(1));
            int count = service.GetAll().Count();
            Assert.AreEqual(0, count);
        }
        [Category("Services")]
        [Test]
        public void Service_Set_Image_Correct()
        {
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("Set").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            service.SetImage("11", "22, 23", "333", "123");
            int count = service.GetAll().Count();
            Assert.AreEqual(1, count);
        }
        [Category("Services")]
        [Test]
        public void Service_Get_Last_Id_Correct()
        {
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("LastId").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            ctx.Add(new GalleryImage
            {
                Id = 0,
                Title = "11"
            });
            ctx.SaveChanges();
            int id = service.LastId();
            Assert.AreEqual(1, id);
        }
        [Category("Services")]
        [Test]
        public void Service_Get_By_UserName_Correct()
        {
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("UserName").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            ctx.Add(new GalleryImage
            {
                Id = 0,
                Title = "11",
                UserName="33"
            });
            ctx.SaveChanges();
            GalleryImage img = service.GetByUserName("33").First();
            Assert.AreEqual("11", img.Title);
        }
        [Category("Services")]
        [Test]
        public void Service_Get_By_Tag_Correct()
        {
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("UserName").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            service.SetImage("aa", "22, 23", "img", "123");
            service.SetImage("bb", "23,24", "png", "123");
            int count=service.GetByTag("23").Count();
            Assert.AreEqual(2, count);
        }
    }
}