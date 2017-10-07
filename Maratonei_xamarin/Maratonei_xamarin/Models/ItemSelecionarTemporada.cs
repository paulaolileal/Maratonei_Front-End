using System.Collections.Generic;
using TraktApiSharp.Objects.Get.Shows.Episodes;
using TraktApiSharp.Objects.Get.Shows.Seasons;

namespace Maratonei_xamarin.Models {
    public class ItemSelecionarTemporada : BaseDataObject {
        private TraktSeason _season;
        private List<TraktEpisode> _episodes;
        private bool _selecionado;
        private int _episodiosSelecionados;

        public TraktSeason Season {
            get => _season;
            set => SetProperty( ref _season, value );
        }

        public bool Selecionado {
            get => _selecionado;
            set => SetProperty( ref _selecionado, value );
        }

        public List<TraktEpisode> Episodes
        {
            get => _episodes;
            set => SetProperty(ref _episodes, value);
        }

        public int EpisodiosSelecionados
        {
            get => _episodiosSelecionados;
            set => SetProperty(ref _episodiosSelecionados, value);
        }
    }
}

