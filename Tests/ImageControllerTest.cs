using ImageGallary.Data;
using ImageGallery.Controllers;
using ImageGalleryServises;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ImageGallary.Data.Model;
using ImageGallery.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Tests
{
    [TestFixture]
    class ImageControllerTest
    {
        [Category("ImageController")]
        [Test]
        [Obsolete]
        public async Task UpdateImage_GetViewResult() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("UpdateImage_GetViewResult").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            var mockEnvironment= new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11"
            });
            ctx.SaveChanges();
            ImageController controller = new ImageController(service, mockEnvironment.Object);
            // Act

            var result = await controller.UpdateImage(1);
            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Category("ImageController")]
        [Test]
        [Obsolete]
        public async Task UpdateImage_ModelHasCorrectType()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("UpdateImage_ModelHasCorrectType").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11"
            });
            ctx.SaveChanges();
            ImageController controller = new ImageController(service, mockEnvironment.Object);
            // Act

            ViewResult result = await controller.UpdateImage(1) as ViewResult;
            var Img = result.ViewData.Model;
            // Assert
            Assert.IsInstanceOf<GalleryImage>(Img);
        }
        [Category("ImageController")]
        [Test]
        [Obsolete]
        public async Task UpdateImage_ModelHasCorrectData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("UpdateImage_ModelHasCorrectData").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11"
            });
            ctx.SaveChanges();
            ImageController controller = new ImageController(service, mockEnvironment.Object);
            // Act

            ViewResult result = await controller.UpdateImage(1) as ViewResult;
            GalleryImage Image = result.ViewData.Model as GalleryImage;
            // Assert
            Assert.AreEqual("11", Image.Title);
        }
        [Category("ImageController")]
        [Test]
        [Obsolete]
        public async Task UpdateImagePost_GetViewResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("Test1_ImageController").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            var mockEnvironment = new Mock<IWebHostEnvironment>();


            Bitmap bitmap = new Bitmap(10, 10, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Image copy = bitmap;

            string newPath = Environment.CurrentDirectory +"/test/newImageName.jpg";
            copy.Save(newPath);

            mockEnvironment
                .Setup(m => m.WebRootPath).Returns(Environment.CurrentDirectory+"/test/");
            ImageController controller = new ImageController(service, mockEnvironment.Object);
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11",
                Url= "newImageName.jpg"
            });
            ctx.SaveChanges();
            var fileMock = new Mock<IFormFile>();

            var file = new Mock<IFormFile>();
            var sourceImg = File.OpenRead("C:\\Users\\PC\\Source\\Repos\\andr1312e\\ImageGallery\\ImageGallery\\wwwroot\\gallery\\1.jpeg");
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(sourceImg);
            writer.Flush();
            stream.Position = 0;
            var fileName = "QQ.png";
            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.FileName).Returns(fileName);
            file.Setup(f => f.Length).Returns(stream.Length);
            file.Setup(f => f.ContentType).Returns("image/jpeg");
            // Act

            var result = await controller.UpdateImage(1, "122", file.Object);
            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var IRes = (ViewResult)result;
            var res = IRes.ViewData.Model as GalleryImage;
            string a="2";
        }





        [Category("ImageController")]
        [Test]
        [Obsolete]
        public async Task DeleteImage_GetViewResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("DeleteImage_GetViewResult").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11",
                Url = ""

            }) ;
            ctx.SaveChanges();
            ImageController controller = new ImageController(service, mockEnvironment.Object);
            // Act

            var result = await controller.Delete(1);
            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Category("ImageController")]
        [Test]
        [Obsolete]
        public async Task DeleteImage_ImageDeleted()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ImageGalleryDbContext>().UseInMemoryDatabase("DeleteImage_ImageDeleted").Options;
            var ctx = new ImageGalleryDbContext(options);
            IImage service = new Service(ctx);
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");
            
            ctx.Add(new GalleryImage
            {
                Id = 1,
                Title = "11"
            });
            ctx.SaveChanges();
            ImageController controller = new ImageController(service, mockEnvironment.Object);
            // Act
            try
            {
                await controller.Delete(1);
                Assert.Fail();
            }
            catch (Exception) { }
            // Assert
        }
    }
}
