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
    public partial class LoginPage : ContentPage {
        public LoginViewModel g_LoginViewModel;
        public LoginPage() {
            InitializeComponent();
            BindingContext = g_LoginViewModel = new LoginViewModel();
        }

        private async void LoginButton_OnClicked( object sender, EventArgs e )
        {
            g_LoginViewModel.IsBusy = true;
            if (!String.IsNullOrEmpty(g_UserEntry.Text) && !String.IsNullOrEmpty(g_PasswordEntry.Text))
            {
                User v_User = new User()
                {
                    Nome = g_UserEntry.Text,
                    Senha = g_PasswordEntry.Text
                };

                var v_ResultLogin = await g_LoginViewModel.AutenticaUsuario(v_User);
            }
            else
            {
                await DisplayAlert("Erro", "Usuário ou senha não preenchidos", "Ok");
            }
            g_LoginViewModel.IsBusy = false;
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