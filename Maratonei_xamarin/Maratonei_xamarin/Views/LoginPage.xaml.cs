using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class LoginPage : ContentPage {
        public LoginViewModel g_LoginViewModel;
        public LoginPage() {
            InitializeComponent();
            BindingContext = g_LoginViewModel = new LoginViewModel();
        }

        private void LoginButton_OnClicked( object sender, EventArgs e ) {
            g_LoginViewModel.IsBusy = !g_LoginViewModel.IsBusy;
        }

        private async void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            var v_RegisterPage = new RegisterPage();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(v_RegisterPage);
            });
        }
    }
}