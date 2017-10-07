using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.ViewModels;
using TraktApiSharp.Objects.Get.Shows.Seasons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class SelecionarTemporadas : ContentPage {
        public EventHandler<List<ItemSelecionarTemporada>> ItensSelecioncados;

        private SelecionarTemporadaViewModel viewModel;
        public SelecionarTemporadas( uint idsTrakt ) {
            InitializeComponent();
            BindingContext = viewModel = new SelecionarTemporadaViewModel( idsTrakt );
        }

        private void GroupedView_OnItemTapped( object sender, ItemTappedEventArgs e ) {
            var selectedSeason = ( (ListView) sender ).SelectedItem as ItemSelecionarTemporada;
            var page = new SelecionarEpisodio( viewModel.g_TraktSHow.TraktSearchResult, selectedSeason );
            page.ItensSelecioncados += ( o, list ) => {
                selectedSeason.Episodes = list;

                var s = list.Aggregate( "", ( current, traktEpisode ) => current + traktEpisode.Number + ", " );
                var toastConfig = new ToastConfig( "Selecionados Episodios : " + s );
                toastConfig.SetDuration( 3000 );
                toastConfig.SetBackgroundColor( System.Drawing.Color.FromArgb( 12, 131, 193 ) );

                UserDialogs.Instance.Toast( toastConfig );

                viewModel.atualizarTemporada( selectedSeason, list );
            };
            Device.BeginInvokeOnMainThread( async () => {
                await Navigation.PushModalAsync( new NavigationPage( page ) );
            } );
        }

        private void MenuItem_OnClicked( object sender, EventArgs e ) {
            ItensSelecioncados( this, viewModel.g_SeasonsList.Where( a => a.Selecionado || a.EpisodiosSelecionados > 0 ).ToList() );
            Navigation.PopModalAsync();
        }

        private void Switch_OnToggled( object sender, ToggledEventArgs e ) {
            var v_switch = sender as Switch;
            var v_item = v_switch.BindingContext as ItemSelecionarTemporada;
            if( v_item.Season.TotalEpisodesCount != null ) {
                v_item.EpisodiosSelecionados = (int) v_item.Season.TotalEpisodesCount;
            }

        }
    }
}