using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Infrastrucure
{
    public class CustomUserValidation:IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            if (!user.Email.ToLower().EndsWith("@mail.com")) 
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else 
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code="EmailDomainError",
                    Description="Only example.com allowed"
                }));
            }
        }
    }
}
