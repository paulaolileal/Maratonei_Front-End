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
using DLToolkit.Forms.Controls;
using Maratonei_xamarin.Services;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class MasterDetailPage1Detail : ContentPage {
        // TraktClient client;
        // TvDbClient tvclient;
        public ObservableCollection<string> Items = new ObservableCollection<string>();

        public MasterDetailPage1Detail() {
            InitializeComponent();
            BindingContext = this;
            lista.FlowItemsSource = Items;
            getTrends();
        }
        public async void getTrends()
        {
            IsBusy = true;
            var temp = new List<string>();
            try {

                var trendingShowsTop10 =
                    await APIs.Instance.MainTraktClient.Shows.GetTrendingShowsAsync(
                        new TraktExtendedInfo { Full = true } );

                foreach( var traktTrendingShow in trendingShowsTop10 ) {
                    temp.Add( await APIs.Instance.PegarImagem( traktTrendingShow.Show.Ids.Tvdb ) );
                }
                foreach( var s in temp ) {
                    Items.Add( s );
                }

            }
            catch( Exception ex ) {
                Debug.WriteLine( ex.StackTrace );
            }
            IsBusy = false;
        }

        private void lista_ItemTapped( object sender, ItemTappedEventArgs e ) {
            var item = sender as FlowListView;
        }
        
    }
}