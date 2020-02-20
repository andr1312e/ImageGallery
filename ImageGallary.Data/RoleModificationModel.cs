using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImageGallary.Data
{
    public class RoleModificationModel
    {
        [Required]
        public string RoleName { set; get; }
        public string RoleId { set; get; }
        public string[] IdsToAdd { set; get; }
        public string[] IdsToDelte { set; get; }
    }
}
