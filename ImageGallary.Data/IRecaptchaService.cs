using ImageGallery.Data.ReCapcha;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Data
{
    public interface IRecaptchaService
    {
        public  Task<RecaptchaResponse> Validate(IFormCollection form);
    }
}
