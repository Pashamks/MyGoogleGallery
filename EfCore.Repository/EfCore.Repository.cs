using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGooglegallery.EfCore.Repository
{
    public class EfCore
    {
        public void AddNewPicture()
        {

        }
        protected EfDbContext GetContext()
        {
            return new EfDbContext();
        }
    }
}
