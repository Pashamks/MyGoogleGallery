using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGoogleGallery.Shared.Models
{
    public class PhotoResponce
    {
        public List<PhotoUrlResponce> hits { get; set; }
    }
    public class PhotoUrlResponce
    {
        public string previewURL { get; set; }
    }
}
