using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.ViewModels;
using TraktApiSharp.Objects.Get.Shows;
using TraktApiSharp.Objects.Get.Shows.Episodes;
using TraktApiSharp.Objects.Get.Shows.Seasons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class SelecionarEpisodio : ContentPage
    {
        
        public EventHandler<List<TraktEpisode>> ItensSelecioncados;
        List<TraktEpisode> Selecionados = new List<TraktEpisode>();

        public SelecionarEpisodioViewModel ViewModel;

        public SelecionarEpisodio(TraktShow show, SelecionarTemporadaViewModel.ItemSelecionarTemporada season ) {
            InitializeComponent();
            BindingContext = ViewModel = new SelecionarEpisodioViewModel(show, season);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            foreach (var itemSelecionarItemse in ViewModel.g_EpisodesList)
            {
                if(itemSelecionarItemse.Selecionado)
                    Selecionados.Add(itemSelecionarItemse.Episode);
            }
            ItensSelecioncados?.Invoke( this, Selecionados );
            Navigation.PopModalAsync();
        }

        private void Button_OnClicked_SelecionatTudo(object sender, EventArgs e)
        {
            ViewModel.SelecionarTudo();
        }
    }
}