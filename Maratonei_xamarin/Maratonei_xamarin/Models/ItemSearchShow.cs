using TraktApiSharp.Objects.Get.Shows;

namespace Maratonei_xamarin.Models {
    public class ItemSearchShow : BaseDataObject {

        TraktShow traktSearchResult;
        public TraktShow TraktSearchResult {
            get { return traktSearchResult; }
            set { SetProperty( ref traktSearchResult, value ); }
        }

        string showImage;
        public string ShowImage {
            get { return showImage; }
            set { SetProperty( ref showImage, value ); }
        }
        
    }
}
