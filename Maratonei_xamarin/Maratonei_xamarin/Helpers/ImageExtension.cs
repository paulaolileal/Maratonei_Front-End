using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maratonei_xamarin.Helpers {
    public static class ImageExtension {
        public static string getImageUrl(this TvDbSharper.Dto.Image img) {
            return "http://thetvdb.com/banners/" + img.Thumbnail;
        }
    }
}
