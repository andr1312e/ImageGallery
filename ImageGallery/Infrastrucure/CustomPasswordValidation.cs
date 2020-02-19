using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Infrastrucure
{
    public class CustomPasswordValidation:IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code="PasswordContainsName", Description="Password can not contains name"
                });
            }
            if ((password.ToLower().Contains(user.Email.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsEmail",
                    Description = "Password can not contains description"
                });
            }
            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
