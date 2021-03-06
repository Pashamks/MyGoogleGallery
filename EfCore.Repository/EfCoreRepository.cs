using MyGooglegallery.EfCore.Repository.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGooglegallery.EfCore.Repository
{
    public class EfCoreRepository
    {
        public async void AddNewPicture(UserPhoto userPhoto)
        {
            userPhoto.PhotoType = userPhoto.PhotoType.ToLower();
            using (var ctxt = GetContext())
            {
                if(!ctxt.UserPhotos.Select(x => x.PhotoUrl).Contains(userPhoto.PhotoUrl))
                    ctxt.UserPhotos.Add(userPhoto);
                else
                {
                    var obj = ctxt.UserPhotos.Where(x => x.PhotoUrl == userPhoto.PhotoUrl).FirstOrDefault();
                    obj.SearchingCounter++;
                    ctxt.UserPhotos.Update(obj);
                }
                await ctxt.SaveChangesAsync();
            }
        }
        
        public async Task<bool> Login(Users user)
        {
            using (var ctxt = GetContext())
            {
                if (ctxt.Users.Where(x => x.Email == user.Email && x.Password == user.Password).Count() > 0)
                    return true;
                var user_in_db = ctxt.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if(user_in_db == null && !string.IsNullOrEmpty(user_in_db.Email) && !string.IsNullOrEmpty(user_in_db.Password))
                {
                    ctxt.Users.Add(user);
                    await ctxt.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }
        public List<UserPhoto> GetAllPictures()
        {
            using (var ctxt = GetContext())
            {
                return ctxt.UserPhotos.ToList();
            }
        }
        public List<UserPhoto> GetPicturesByType(string type)
        {
            using (var ctxt = GetContext())
            {
                return ctxt.UserPhotos.Where(x => x.PhotoType == type).ToList();
            }
        }
        protected EfDbContext GetContext()
        {
            return new EfDbContext();
        }
    }
}
