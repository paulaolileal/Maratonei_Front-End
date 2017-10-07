
using System;
using System.Collections.Generic;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public bool InLoading {
            get { return g_InLoading; }
            set { SetProperty( ref g_InLoading, value ); }
        }

        public SearchShowViewModel() {
            g_SearchResult = new ObservableCollection<ItemSearchShow>();
        }

        public async void Search( string p_Param ) {
            InLoading = true;

            g_SearchResult.Clear();

            var result = await APIs.Instance.MainTraktClient.Search.GetTextQueryResultsAsync(
                 TraktApiSharp.Enums.TraktSearchResultType.Show,
                 p_Param.Replace( " ", "-" ),
                 extendedInfo: new TraktExtendedInfo() { Full = true }
                 );

            foreach( var item in result ) {
                ////// var img = await APIs.Instance.PegarImagem(item.Show.Ids.Tvdb);
                //    "no_image.png";
                //if( item.Show.Ids.Tvdb != null ) {
                //    try {
                //        var i = await APIs.Instance.MainTvDbClient.Series.GetImagesAsync( 
                //            (int) item.Show.Ids.Tvdb, 
                //            new TvDbSharper.Dto.ImagesQuery() { KeyType = TvDbSharper.Dto.KeyType.Poster } 
                //            );
                //        if( i.Data.Length > 0 ) {
                //            img = ( i.Data[i.Data.Length - 1].getImageUrl() );
                //        }
                //    }
                //    catch( Exception e ) {
                //        Debug.WriteLine( e );
                //    }

                //}

                g_SearchResult.Add(
                    new ItemSearchShow() {
                        /////ShowImage = img,
                        TraktSearchResult = item.Show
                    }
                );
            }
            InLoading = false;
        }

    }
}
