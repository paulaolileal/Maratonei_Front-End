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
    public partial class SelecionarEpisodios : ContentPage
    {
        private SelecionarEpisodiosViewModel viewModel;
        public SelecionarEpisodios() {
            InitializeComponent();
            BindingContext = viewModel = new SelecionarEpisodiosViewModel();
        }
    }
}