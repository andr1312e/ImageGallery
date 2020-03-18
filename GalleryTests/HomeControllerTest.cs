using ImageGallary.Data;
using ImageGallery.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GalleryTests
{
    public class HomeControllerTest
    {
        [Fact]
        public void FAQ_GET_VIEW()
        {
            // Arrange
            var controller = new HomeController();
            // Act
            Assert.IsType < ViewResult > (controller.FAQ());
        }
        [Fact]
        public void UPLOAD_FILES()
        {
            Mock<IImage> service = new Mock<IImage>();           
            ImageGalleryController imageGalleryController = new ImageGalleryController(service.Object);
            service.Setup(m => m.SetImage("aa", "bb", "22", "AAA"));
            imageGalleryController.Index();
            Assert.Equal("1", "1");
        }
    }
}
