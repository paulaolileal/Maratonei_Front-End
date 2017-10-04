using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.Services;
using TraktApiSharp.Objects.Get.Shows;
using TraktApiSharp.Objects.Get.Shows.Seasons;
using TraktApiSharp.Requests.Params;

namespace Maratonei_xamarin.ViewModels {
    public partial class SelecionarEpisodioViewModel : BaseViewModel{
        public ItemSelecionarTemporada g_TraktSeason { get; set; }
        public TraktShow g_TraktSHow { get; set; }
        public ObservableCollection<ItemSelecionarEpisodio> g_EpisodesList { get; set; }
        public SelecionarEpisodioViewModel( TraktShow gTraktShow, ItemSelecionarTemporada gTraktSeason ) {
            g_TraktSeason = gTraktSeason;
            g_TraktSHow = gTraktShow;
            g_EpisodesList = new ObservableCollection<ItemSelecionarEpisodio>();
            GetEpisodes();
        }

        private async void GetEpisodes()
        {
            IsBusy = true;
            if( g_TraktSeason.Season.Number != null ) {
                var episodes = await APIs.Instance.MainTraktClient.Seasons.GetSeasonAsync( g_TraktSHow.Ids.Trakt.ToString(), (int) g_TraktSeason.Season.Number, new TraktExtendedInfo { Episodes = true } );
                foreach( var traktEpisode in episodes ) {
                    g_EpisodesList.Add( new ItemSelecionarEpisodio() { Episode = traktEpisode, Selecionado = g_TraktSeason.Selecionado } );
                }
            }
            IsBusy = false;
        }

        public void SelecionarTudo()
        {
            foreach (var x1 in g_EpisodesList)
            {
                x1.Selecionado = true;
            }
        }
    }
}
