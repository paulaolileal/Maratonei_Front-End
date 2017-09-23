using Maratonei_xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Maratonei_xamarin.Models;
using TraktApiSharp.Objects.Get.Shows;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class SearchShows : ContentPage {
        SearchShowViewModel g_viewModel;
        public SearchShows() {
            InitializeComponent();
            BindingContext = g_viewModel = new SearchShowViewModel();
            //list_view_search_shows.ItemsSource = g_viewModel.g
        }

        private void search_bar_shows_SearchButtonPressed( object sender, EventArgs e ) {
            g_viewModel.Search(search_bar_shows.Text);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var show = (btn.BindingContext as ItemSearchShow).TraktSearchResult;

            var toastConfig = new ToastConfig( "Toasting..." );
            toastConfig.SetDuration( 3000 );
            toastConfig.SetBackgroundColor( System.Drawing.Color.FromArgb( 12, 131, 193 ) );

            UserDialogs.Instance.Toast( toastConfig );

            // TODO adicionar serie à maratorna
        }
    }
}