using MyGooglegallery.EfCore.Repository.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGoogleGallery.Shared.Models
{
    public class UserPhotoModel
    {
        public string PhotoUrl { get; set; }
        public string PhotoType { get; set; }
        public int SearchingCounter { get; set; } = 0;

        public static  implicit operator UserPhoto(UserPhotoModel userPhotoModel)
        {
            return new UserPhoto { PhotoType = userPhotoModel.PhotoType, PhotoUrl = userPhotoModel.PhotoUrl, SearchingCounter = userPhotoModel.SearchingCounter };
        }
    }
}
