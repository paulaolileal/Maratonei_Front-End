using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.Services;
using TraktApiSharp;
using TraktApiSharp.Objects.Get;
using TraktApiSharp.Extensions;
using TraktApiSharp.Objects.Get.Shows;
using TraktApiSharp.Objects.Get.Shows.Episodes;
using TraktApiSharp.Objects.Get.Shows.Seasons;
using TraktApiSharp.Requests.Params;

namespace Maratonei_xamarin.ViewModels {
    class SelecionarEpisodiosViewModel : BaseViewModel {
        public ObservableCollection<TraktSeason> g_SeasonsList { get; set; }
        public TraktShow g_TraktSHow { get; set; }

        public SelecionarEpisodiosViewModel() {
            g_SeasonsList = new ObservableCollection<TraktSeason>();
            BuscarSerie( "353" );
        }

        public async void BuscarSerie( string id ) {
            g_TraktSHow = await APIs.Instance.MainTraktClient.Shows.GetShowAsync( id, new TraktExtendedInfo { Full = true } );
            var seasons = await APIs.Instance.MainTraktClient.Seasons.GetAllSeasonsAsync( g_TraktSHow.Ids.Trakt.ToString() );
            foreach( var traktSeason in seasons ) {
                g_SeasonsList.Add( traktSeason );
            }

        }
    }
}

