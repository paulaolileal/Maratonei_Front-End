using Maratonei_xamarin.Models;
using Maratonei_xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Maratonei_xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelecionarComidasPage : ContentPage
    {
        private ObservableCollection<SolucaoModel> g_SolucaoList;

        public SelecionarComidasViewModel ViewModel { get; }

        public SelecionarComidasPage(double maratona, ObservableCollection<SolucaoModel> g_SolucaoList)
        {
            InitializeComponent();
            BindingContext = ViewModel = new SelecionarComidasViewModel(g_SolucaoList) { Maratona = maratona };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var co = (sender as Button).BindingContext as Comida;
            ViewModel.RemoverComida(co);
        }

        private void Button_Clicked_Add_Comidas(object sender, EventArgs e)
        {
            ViewModel.AddComida(Entry_Nome.Text, Entry_Quantidade_Por_Epi.Text);
            Entry_Nome.Text = Entry_Quantidade_Por_Epi.Text = "";
        }

        private async void Button_OnClicked_ok(object sender, EventArgs e)
        {
            try { 
            
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushModalAsync(new NavigationPage(new SolucaoLancharPage(await ViewModel.Lanchar())));
                });
            }
            catch
            {
                await DisplayAlert("", "Não foi possível calcular", "ok");
            }
        }
    }
}