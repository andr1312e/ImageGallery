using ImageGallary.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallary.Data
{
    public interface IImage
    {
        IEnumerable<GalleryImage> GetAll();
        GalleryImage GetById(int id);
        IEnumerable<GalleryImage> GetByTag(string Tag);
        IEnumerable<GalleryImage> GetByUserName(string CurrentUserName);
        void Delete(GalleryImage img);
        int LastId();
        public void EditImage(GalleryImage img, string url);
        public void Rename(GalleryImage img, string name);
        void SetImage(string title, string tags, string path, string user);
        public string SetImageAuthorInDetail(string user, string ImageUserName, bool AdminRole);
    }
}
