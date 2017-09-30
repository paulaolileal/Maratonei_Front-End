using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel g_LoginViewModel;
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            //g_Logo.Source = "Logo-Maratonei.png";
            InitializeComponent();
            BindingContext = g_LoginViewModel = new LoginViewModel();
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            g_LoginViewModel.IsBusy = true;
            if (!String.IsNullOrEmpty(g_UserEntry.Text) && !String.IsNullOrEmpty(g_PasswordEntry.Text))
            {
                User v_User = new User()
                {
                    Nome = g_UserEntry.Text,
                    Senha = g_PasswordEntry.Text
                };

                try
                {
                    var v_ResultLogin = await g_LoginViewModel.AutenticaUsuario(v_User);
                    if (string.IsNullOrEmpty(v_ResultLogin))
                    {
                        await DisplayAlert("Falha na autenticação", "Usuário e/ou senha errados", "Tentar novamente");
                    }
                    else
                    {
                        v_User = JsonConvert.DeserializeObject<User>(v_ResultLogin);
                        v_User.EstaLogado = true;
                        g_LoginViewModel.ArmazenaUsuarioLocal(v_User);
                        
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Navigation.PushModalAsync(new MasterDetailPage1(v_User));
                        });
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro",
                        "Ocorreu uma exceção não prevista ao tentar realizar o login, favor contactar os desenvolvedores informando esta mensagem: " + ex.Message,
                        "Ok");
                }
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
                await Navigation.PushAsync(new NavigationPage(v_RegisterPage));
            });
        }
    }
}
