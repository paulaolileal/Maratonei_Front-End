using TraktApiSharp.Objects.Get.Shows.Episodes;

namespace Maratonei_xamarin.Models {
    public class ItemSelecionarEpisodio : BaseDataObject {
        public TraktEpisode Episode {
            get => _episode;
            set => SetProperty( ref _episode, value );
        }

        private bool _selecionado;
        private TraktEpisode _episode;

        public bool Selecionado {
            get => _selecionado;
            set => SetProperty( ref _selecionado, value );

        }
    }
}
