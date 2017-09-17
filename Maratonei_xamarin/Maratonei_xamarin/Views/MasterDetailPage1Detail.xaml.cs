using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraktApiSharp;
using TraktApiSharp.Requests.Params;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using TvDbSharper;
using System.Collections.ObjectModel;
using Maratonei_xamarin.Helpers;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class MasterDetailPage1Detail : ContentPage {
        TraktClient client;
        TvDbClient tvclient;
        ObservableCollection<string> Items = new ObservableCollection<string>();

        public MasterDetailPage1Detail() {
            InitializeComponent();
            lista.FlowItemsSource = Items;
            client = new TraktClient(
                "291a8dd6ebf31265856c34b0fc6e9be0f81269e82de0f267e654bcfc6bf2a857",
                "a4f0a9ebf050c37f95a3ee18fffc96aa33d5b188e6c526f81a5a1c2572384ff1"
                );
            tvclient = new TvDbClient();
            tvclient.AcceptedLanguage = "en";
            getTrends();
        }
        public async void getTrends() {
            try {
                await tvclient.Authentication.AuthenticateAsync( "FEF236EED282A656" );
                var trendingShowsTop10 = await client.Shows.GetTrendingShowsAsync( new TraktExtendedInfo().SetFull(), null, 10 );

                foreach( var trendingShow in trendingShowsTop10 ) {

                    var show = trendingShow.Show;
                    var tv = await tvclient.Series.GetAsync( (int) show.Ids.Tvdb.Value );
                    var i = await tvclient.Series.GetImagesAsync( (int) show.Ids.Tvdb.Value, new TvDbSharper.Dto.ImagesQuery() { KeyType = TvDbSharper.Dto.KeyType.Poster } );

                    if(i.Data.Length > 0)
                        Items.Add( i.Data[i.Data.Length-1].getImageUrl() );

                    labelp.Text = labelp.Text + "\n" + ( $"Show: {show.Title} / Watchers: {trendingShow.Watchers}." );                   

                }
            }
            catch( Exception ex ) {
                Debug.WriteLine( ex.StackTrace );
            }
        }
    }
}