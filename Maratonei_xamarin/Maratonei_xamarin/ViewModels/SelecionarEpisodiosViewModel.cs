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
using TraktApiSharp.Requests.Params;

namespace Maratonei_xamarin.ViewModels {
    class SelecionarEpisodiosViewModel : BaseViewModel {
        public ObservableCollection<SeasonModel> g_SeasonsList { get; set; }

        public SelecionarEpisodiosViewModel() {
            g_SeasonsList = new ObservableCollection<SeasonModel>();
            BuscarSerie( "353" );

        }

        public async void BuscarSerie( string id ) {
            var show = await APIs.Instance.MainTraktClient.Shows.GetShowAsync( id, new TraktExtendedInfo { Full = true } );
            var seasons = await APIs.Instance.MainTraktClient.Seasons.GetAllSeasonsAsync( show.Ids.Trakt.ToString(),
                    new TraktExtendedInfo { Episodes = true } );
            foreach( var season in seasons ) {
                g_SeasonsList.Add( new SeasonModel( season.Episodes ) { Season = season } );
            }
        }
    }
}

