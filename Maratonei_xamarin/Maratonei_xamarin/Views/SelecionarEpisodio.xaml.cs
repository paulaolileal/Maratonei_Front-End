using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.ViewModels;
using TraktApiSharp.Objects.Get.Shows;
using TraktApiSharp.Objects.Get.Shows.Seasons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class SelecionarEpisodio : ContentPage
    {
        public SelecionarEpisodioViewModel ViewModel;

        public SelecionarEpisodio(TraktShow show, TraktSeason season) {
            InitializeComponent();
            BindingContext = ViewModel = new SelecionarEpisodioViewModel(show, season);
        }
    }
}