using Maratonei_xamarin.Models;
using Maratonei_xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelecionarComidasPage : ContentPage
    {
        public SelecionarComidasViewModel ViewModel { get; }
        public SelecionarComidasPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SelecionarComidasViewModel();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var co = (sender as Button).BindingContext as Comida;
            ViewModel.RemoverComida(co);
        }

        private void Button_Clicked_Add_Comidas(object sender, EventArgs e)
        {
            ViewModel.AddComida(Entry_Nome.Text, Entry_Quantidade.Text);
        }

        private void Button_OnClicked_ok(object sender, EventArgs e)
        {
            ViewModel.EnviarRequisicao();
        }
    }
}