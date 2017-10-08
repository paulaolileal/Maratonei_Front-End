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
    public partial class RegisterPage : ContentPage
    {
        public RegisterViewModel g_RegisterViewModel;
        public RegisterPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            BindingContext = g_RegisterViewModel = new RegisterViewModel();
        }

        private async void G_SubmitButton_OnClicked(object sender, EventArgs e)
        {
            if (ValidaPreenchimento())
            {
                if (ValidaSenha())
                {
                    var v_User = new User()
                    {
                        Nome = g_UserEntry.Text,
                        Senha = g_UserEntry.Text,
                        TraktUser = g_TraktUserEntry.Text
                    };
                    g_RegisterViewModel.IsBusy = true;

                    if (await g_RegisterViewModel.ValidaUsuarioTrakt(v_User.TraktUser))
                    {
                        try
                        {
                            var v_InvalidUser = "This username is already being used.";
                            var v_ResultCadastro = await g_RegisterViewModel.CadastraNovoUsuario(v_User);

                            if (!v_InvalidUser.Equals(v_ResultCadastro))
                            {
                                v_User = JsonConvert.DeserializeObject<User>(v_ResultCadastro);
                                g_RegisterViewModel.InsereNovoUsuario(v_User);

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Navigation.PushModalAsync(new MasterDetailPage1(v_User));
                                });
                            }

                            else
                            {
                                await DisplayAlert("Aviso", $"O usuário {v_User.Nome} já está em uso", "Ok");
                            }
                        }
                        catch (Exception exc)
                        {
                            await DisplayAlert("Erro", "Ocorreu uma exceção não prevista no cadastro de novo usuário, por favor contacte os desenvolvedores informando esta mensagem: " + exc.Message, "Ok");
                        } 
                    }

                    else
                    {
                        await DisplayAlert("Aviso", $"O usuário Trakt {v_User.TraktUser} já está em uso", "Ok");
                    }

                    g_RegisterViewModel.IsBusy = false;
                }
                else
                {
                    await DisplayAlert("Atenção", "A senha digitada não confere com a confirmação de senha", "Ok");
                    g_PasswordEntry.Text = g_ConfirmPasswordEntry.Text = "";
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Por favor preenhcha todos os campos de cadastro", "Ok");
            }
        }

        private bool ValidaPreenchimento()
        {
            return (!String.IsNullOrWhiteSpace(g_ConfirmPasswordEntry.Text) &&
                    !String.IsNullOrWhiteSpace(g_EmailEntry.Text) &&
                    !String.IsNullOrWhiteSpace(g_PasswordEntry.Text) &&
                    !String.IsNullOrWhiteSpace(g_TraktUserEntry.Text) &&
                    !String.IsNullOrWhiteSpace(g_UserEntry.Text)
                );
        }

        private bool ValidaSenha()
        {
            return g_PasswordEntry.Text.Equals(g_ConfirmPasswordEntry.Text);
        }

        private void Trakt_Register_Button(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread((() =>
            {
                Device.OpenUri(new Uri("https://trakt.tv/auth/join"));
            }));
        }
    }
}