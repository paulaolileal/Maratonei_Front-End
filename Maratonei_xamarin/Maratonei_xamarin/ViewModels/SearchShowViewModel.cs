
using System;
using System.Collections.Generic;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Maratonei_xamarin.Services;
using TraktApiSharp;
using TraktApiSharp.Objects.Get.Shows;
using TraktApiSharp.Requests.Params;
using TvDbSharper;

namespace Maratonei_xamarin.ViewModels {
    class SearchShowViewModel : BaseViewModel {
        public ObservableCollection<ItemSearchShow> g_SearchResult { get; set; }
        public List<TraktShow> g_listaSelecionados = new List<TraktShow>();
        private bool g_InLoading;
        private bool g_naoPesquisado;
        private bool g_NadaEncontrado;

        public bool InLoading {
            get => g_InLoading;
            set => SetProperty( ref g_InLoading, value );
        }

        public bool NadaEncontrado {
            get => g_NadaEncontrado;
            set => SetProperty( ref g_NadaEncontrado, value );
        }

        public bool NaoPesquisado {
            get => g_naoPesquisado;
            set => SetProperty( ref g_naoPesquisado, value );
        }

        public SearchShowViewModel()
        {
            NaoPesquisado = true;
            NadaEncontrado = false;
            g_SearchResult = new ObservableCollection<ItemSearchShow>();
        }

        public async void Search( string p_Param ) {
            NaoPesquisado = false;
            InLoading = true;

            g_SearchResult.Clear();

            var result = await APIs.Instance.MainTraktClient.Search.GetTextQueryResultsAsync(
                 TraktApiSharp.Enums.TraktSearchResultType.Show,
                 p_Param.Replace( " ", "-" ),
                 extendedInfo: new TraktExtendedInfo() { Full = true }
                 );
            NadaEncontrado = !result.Any();
            foreach( var item in result ) {
                g_SearchResult.Add(
                    new ItemSearchShow() {
                        TraktSearchResult = item.Show
                    }
                );
            }
            InLoading = false;
        }

    }
}
