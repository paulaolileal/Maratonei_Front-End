using Maratonei_xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}