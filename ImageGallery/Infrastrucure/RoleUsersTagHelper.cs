using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Infrastrucure
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper:TagHelper
    {
        private UserManager<AppUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        public RoleUsersTagHelper(UserManager<AppUser> user, RoleManager<IdentityRole> role)
        {
            UserManager = user;
            RoleManager = role;
        }
        [HtmlAttributeName("identity-role")]
        public string Role { set; get; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            IdentityRole identityRole = await RoleManager.FindByIdAsync(Role);
            if (identityRole != null)
            {
                foreach (var user in UserManager.Users)
                {
                    if (user != null && await UserManager.IsInRoleAsync(user, identityRole.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }
            output.Content.SetContent(names.Count == 0 ? "No users" : string.Join(", ", names));
        }

    }
}
