using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImageGallary.Data
{
    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        public string Email { set; get; }
        [Required]
        [UIHint("password")]
        public string Password { set; get; }
    }
}
