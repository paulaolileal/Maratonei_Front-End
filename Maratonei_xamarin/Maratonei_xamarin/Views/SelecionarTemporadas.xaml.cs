    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using Maratonei_xamarin.Models;
    using Maratonei_xamarin.ViewModels;
    using TraktApiSharp.Objects.Get.Shows.Seasons;
    using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class SelecionarTemporadas : ContentPage
    {
        private SelecionarEpisodiosViewModel viewModel;
        public SelecionarTemporadas() {
            InitializeComponent();
            BindingContext = viewModel = new SelecionarEpisodiosViewModel();
        }

        private void GroupedView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedSeason = ( (ListView) sender ).SelectedItem as TraktSeason;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushModalAsync(new SelecionarEpisodio(viewModel.g_TraktSHow, selectedSeason));
            });
        }
    }
}