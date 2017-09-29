﻿using System;
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
    public class SelecionarTemporadaViewModel : BaseViewModel {
        public class ItemSelecionarTemporada : BaseDataObject {
            private TraktSeason _season;
            private bool _selecionado;

            public TraktSeason Season {
                get { return _season; }
                set { SetProperty( ref _season, value ); }
            }

            public bool Selecionado {
                get { return _selecionado; }
                set { SetProperty( ref _selecionado, value ); }
            }
        }
        public ObservableCollection<ItemSelecionarTemporada> g_SeasonsList { get; set; }
        public ItemSearchShow g_TraktSHow = new ItemSearchShow();

        public SelecionarTemporadaViewModel() {
            g_SeasonsList = new ObservableCollection<ItemSelecionarTemporada>();
            BuscarSerie( "353" );
        }

        public async void BuscarSerie( string id ) {
            var show = await APIs.Instance.MainTraktClient.Shows.GetShowAsync( id, new TraktExtendedInfo { Full = true } );
            g_TraktSHow.TraktSearchResult = show;
            var seasons = await APIs.Instance.MainTraktClient.Seasons.GetAllSeasonsAsync( show.Ids.Trakt.ToString(), new TraktExtendedInfo { Full = true } );
            foreach( var traktSeason in seasons ) {
                g_SeasonsList.Add( new ItemSelecionarTemporada { Season = traktSeason } );
            }

        }

        internal void atualizarTemporada( ItemSelecionarTemporada selectedSeason, List<TraktEpisode> list ) {
            if( list.Count == selectedSeason.Season.TotalEpisodesCount ) {
                selectedSeason.Selecionado = true;
            }
        }
    }
}
