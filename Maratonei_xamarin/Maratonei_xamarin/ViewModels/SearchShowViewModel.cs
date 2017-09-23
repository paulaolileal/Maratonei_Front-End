
using System;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TraktApiSharp;
using TraktApiSharp.Requests.Params;
using TvDbSharper;

namespace Maratonei_xamarin.ViewModels {
    class SearchShowViewModel : BaseViewModel {
        public ObservableCollection<ItemSearchShow> g_SearchResult { get; set; }

        private bool g_InLoading;
        public bool InLoading
        {
            get { return g_InLoading; }
            set { SetProperty( ref g_InLoading, value ); }
        }

        TraktClient client;
        TvDbClient tvclient;

        public SearchShowViewModel() {
            g_SearchResult = new ObservableCollection<ItemSearchShow>();

            client = new TraktClient(
                "291a8dd6ebf31265856c34b0fc6e9be0f81269e82de0f267e654bcfc6bf2a857",
                "a4f0a9ebf050c37f95a3ee18fffc96aa33d5b188e6c526f81a5a1c2572384ff1"
                );
            tvclient = new TvDbClient { AcceptedLanguage = "en" };
        }

        public async void Search( string p_Param )
        {
            InLoading = true;

            await tvclient.Authentication.AuthenticateAsync( "FEF236EED282A656" );

            var result = await client.Search.GetTextQueryResultsAsync(
                TraktApiSharp.Enums.TraktSearchResultType.Show,
                p_Param.Replace( " ", "-" ),
                extendedInfo: new TraktExtendedInfo() { Full = true }
                );

            foreach( var item in result ) {
                var img = "";
                if( item.Show.Ids.Tvdb != null ) {
                    try {
                        var i = await tvclient.Series.GetImagesAsync( 
                            (int) item.Show.Ids.Tvdb, 
                            new TvDbSharper.Dto.ImagesQuery() { KeyType = TvDbSharper.Dto.KeyType.Poster } 
                            );
                        if( i.Data.Length > 0 ) {
                            img = ( i.Data[i.Data.Length - 1].getImageUrl() );
                        }
                    }
                    catch( Exception e ) {
                        Debug.WriteLine( e );
                    }

                }

                g_SearchResult.Add(
                    new ItemSearchShow() {
                        ShowImage = img,
                        TraktSearchResult = item.Show
                    }
                );
            }
            InLoading = false;
        }
    }
}
