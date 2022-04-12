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
            using (var ctxt = GetContext())
            {
                ctxt.UserPhotos.Add(userPhoto);
                await ctxt.SaveChangesAsync();
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
