using Maratonei.Models;
using Maratonei_xamarin.Models;
using System.Collections.ObjectModel;

namespace Maratonei_xamarin.ViewModels
{
    class SolucaoLancharViewModel: BaseViewModel
    {
        double solucao;
        public double Solucao { get => solucao; set => SetProperty(ref solucao, value); }
        public ObservableCollection<Comida> Comidas { get; set; }

        public SolucaoLancharViewModel(GLPKOutput resultado)
        {
            Comidas = new ObservableCollection<Comida>();
            Solucao = resultado.Objectives["Z"];
            foreach (var item in resultado.Objectives)
            {
                Comidas.Add(new Comida { Nome = item.Key, Quantidade = item.Value });
            }
        }

    }
}
