using System.Threading.Tasks;
using Maratonei_xamarin.Services;
using TraktApiSharp.Objects.Get.Shows;

namespace Maratonei_xamarin.Models {
    public class ItemSearchShow : BaseDataObject {

        TraktShow traktSearchResult;
        public TraktShow TraktSearchResult {
            get => traktSearchResult;
            set {
                SetProperty( ref traktSearchResult, value );
                AtualizarImagem();
            }
        }

        string showImage;
        public string ShowImage {
            get => showImage;
            set => SetProperty( ref showImage, value );
        }

        private bool _selecionado;
        public bool Selecionado {
            get => _selecionado;
            set => SetProperty( ref _selecionado, value );
        }

        public async Task AtualizarImagem() {
            ShowImage = await APIs.Instance.PegarImagem( TraktSearchResult.Ids.Tvdb );
        }
    }
}
