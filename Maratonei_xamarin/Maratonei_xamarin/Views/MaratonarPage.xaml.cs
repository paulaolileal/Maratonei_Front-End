using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class MaratonarPage : ContentPage {
        private MaratonarViewModel viewModel;

        private int g_Limit = 2;

        public MaratonarPage( List<TraktApiSharp.Objects.Get.Shows.TraktShow> g_listaSelecionados ) {
            InitializeComponent();
            BindingContext = viewModel = new MaratonarViewModel( g_listaSelecionados );
        }

        private void Entry_OnTextChanged( object sender, TextChangedEventArgs e ) {
            var v_entry = sender as Entry;
            var v_text = v_entry.Text; //Get Current Text
            if( v_text.Length <= 0 ) return;
            if( v_text.Length > g_Limit ) //If it is more than your character restriction
            {
                v_text = v_text.Remove( v_text.Length - 1 ); // Remove Last character
                v_entry.Text = v_text; //Set the Old value
            }
            if( int.TryParse( v_text, out _ ) ) return;
            v_text = v_text.Remove( v_text.Length - 1 ); // Remove Last character
            v_entry.Text = v_text; //Set the Old value
        }


        private void Button_OnClicked( object sender, EventArgs e ) {
            var v_item = ( sender as Button ).BindingContext as ItemMaratonarModel.ListaShowRequisicao;
            if( v_item == null )
                return;
            var v_page = new SelecionarTemporadas( v_item.TraktShow.Ids.Trakt );
            v_page.ItensSelecioncados += ( o, list ) => {
                v_item.ListaTemporadas = list;
            };
            Device.BeginInvokeOnMainThread( async () => {
                await Navigation.PushModalAsync( new NavigationPage( v_page ) );
            } );
        }

        private void Button_Maratonar_OnClicked( object sender, EventArgs e ) {
            AbrirResultado();
        }

        public async void AbrirResultado() {
            Device.BeginInvokeOnMainThread( async () => {
                await Navigation.PushAsync( new SolucaoPage(viewModel.g_Model, await viewModel.Maratonar()) );
            } );
        }
    }
}