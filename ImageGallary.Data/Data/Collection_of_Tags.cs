using ImageGallary.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallary.Data.Data
{
    public static class Collection_of_Tags
    {
        public static readonly Tag Adventure = new Tag()
        {
            Id = 0,
            Description = "Adventure"
        };
        public static readonly Tag Friends = new Tag()
        {
            Id = 1,
            Description = "Friends"
        };
        public static readonly Tag GroupMates = new Tag()
        {
            Id = 2,
            Description = "GroupMates"
        };
        public static readonly Tag Me = new Tag()
        {
            Id = 3,
            Description = "Me"
        };
        
    }
}
