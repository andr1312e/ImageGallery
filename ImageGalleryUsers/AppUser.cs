using System;

namespace ImageGalleryUsers
{
    using Microsoft.AspNetCore.Identity;

    namespace CustomIdentityApp.Models
    {
        public class AppUser : IdentityUser
        {
            public int Year { get; set; }
        }
    }
}
