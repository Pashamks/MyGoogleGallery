using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGooglegallery.EfCore.Repository;
using MyGoogleGallery.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGoogleGallery.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPhotoController : ControllerBase
    {
        private static EfCoreRepository _efCoreRepository = new EfCoreRepository();

        [HttpPost]
        [Route("add_photo")]
        public async Task<UserPhotoModel> AddNewPhoto(UserPhotoModel userPhotoModel)
        {
            //_efCoreRepository.AddNewPicture((UserPhoto)userPhotoModel);
            return userPhotoModel;
        }
        [HttpGet]
        [Route("get_photos")]
        public async Task<string> GetPhotos()
        {
            return "Get";
            //_efCoreRepository.AddNewPicture((UserPhoto)userPhotoModel);
        }
    }
}
