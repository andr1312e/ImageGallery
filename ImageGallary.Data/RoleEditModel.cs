using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallary.Data
{
    public class RoleEditModel
    {
        public IdentityRole Role { set; get; }
        public IEnumerable<AppUser> Members { set; get; }
        public IEnumerable<AppUser> NonMembers { set; get; }
    }
}
