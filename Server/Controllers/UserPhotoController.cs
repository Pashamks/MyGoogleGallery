using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGooglegallery.EfCore.Repository;
using MyGooglegallery.EfCore.Repository.DbModels;
using MyGoogleGallery.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        public async Task<UserPhoto> AddNewPhoto(UserPhotoModel userPhotoModel)
        {
            _efCoreRepository.AddNewPicture((UserPhoto)userPhotoModel);
            return (UserPhoto)userPhotoModel;
        }
        [HttpPost]
        [Route("login")]
        public async Task<bool> Login([FromQuery] string email, [FromQuery] string password)
        {
            var user = new Users { Email = email, Password = password };
            bool val = await _efCoreRepository.Login(user);
            return val ;
        }
        [HttpGet]
        [Route("get_photos")]
        public async Task<List<UserPhoto>> GetPhotos()
        {
            return _efCoreRepository.GetAllPictures();
        }
        [HttpGet]
        [Route("get_chart_data")]
        public async Task<Dictionary<string, int>> GetChartData()
        {
            var res = _efCoreRepository.GetAllPictures();
            var val = res.Select(x => x.PhotoType).Distinct().ToDictionary(k => k.ToLower(), v => res.Where(x => x.PhotoType == v).Count());
            return val;
        }
        [HttpGet]
        [Route("find_photos")]
        public async Task<string> FindPhotos([FromQuery]string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                throw new Exception("searchTerm is empty");

            const string subscriptionKey = "26728888-9c5297e15ee95958b38b15abd";
            const string uriBase = "https://pixabay.com/api/?";
            var uriQuery = uriBase + "key=" + Uri.EscapeDataString(subscriptionKey) + "&q=" + Uri.EscapeDataString(searchTerm) + "&media_type=photo";

            WebRequest request = WebRequest.Create(uriQuery);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<PhotoResponce>(json);

            Random random = new Random();
            int i = random.Next(0, jsonObject.hits.Count);

            return jsonObject.hits[i].previewURL;
        }
    }
}
