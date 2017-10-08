using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class WhatchList : ContentPage {
        public WhatchList() {
            InitializeComponent();
            BindingContext = g_viewModel = new WatchListViewModel();
        }

        private void Button_OnClicked( object sender, EventArgs e ) {

            var v_btn = sender as Button;
            var v_result = (ItemSearchShow) v_btn.BindingContext;
            if( v_result == null ) return;
            var v_show = v_result.TraktSearchResult;
            string msg;
            v_result.Selecionado = !v_result.Selecionado;
            if( v_result.Selecionado ) {
                g_viewModel.g_listaSelecionados.Add( v_result.TraktSearchResult );
                v_btn.Text = "Remover";
                msg = "Adicionado";
            }
            else {
                g_viewModel.g_listaSelecionados.Remove( v_show );
                v_btn.Text = "Add";
                msg = "Removido";
            }

            var toastConfig = new ToastConfig( $"{msg}: {v_show.Title}" );
            toastConfig.SetDuration( 3000 );
            toastConfig.SetBackgroundColor( System.Drawing.Color.FromArgb( 12, 131, 193 ) );

            UserDialogs.Instance.Toast( toastConfig );
        }

        public WatchListViewModel g_viewModel { get; set; }

        private void MenuItem_OnClicked( object sender, EventArgs e ) {
            Device.BeginInvokeOnMainThread( async () => {
                await Navigation.PushAsync( new MaratonarPage( g_viewModel.g_listaSelecionados ) );
            } );
        }
    }
}
