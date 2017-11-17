using Maratonei.Models;
using Maratonei_xamarin.Models;
using System.Collections.ObjectModel;

namespace Maratonei_xamarin.ViewModels
{
    public class SolucaoLancharViewModel: BaseViewModel
    {
        double solucao;
        public double Solucao { get => solucao; set => SetProperty(ref solucao, value); }
        public ObservableCollection<Comida> Comidas { get; set; }
        private string tipoSolucao;
        private string mensagemSolucao;
        public string TipoSolucao { get => tipoSolucao; set => SetProperty(ref tipoSolucao, value); }
        public string MensagemSolucao { get => mensagemSolucao; set => SetProperty(ref mensagemSolucao, value); }

        public SolucaoLancharViewModel(GLPKOutput resultado)
        {
            try
            {
                Comidas = new ObservableCollection<Comida>();
                Solucao = resultado.Objectives["Z"];
                foreach (var item in resultado.Variables)
                {
                    Comidas.Add(new Comida { Nome = item.Key, Quantidade = item.Value });
                }
                switch (resultado.Status)
                {
                    case GLPKOutput.GLPKStatus.NoSolutionValues:
                        TipoSolucao = "Sem Solução";
                        mensagemSolucao = "Algo deu errado ao calcular o resultado";
                        break;
                    case GLPKOutput.GLPKStatus.FeasibleContinuousRelaxation:
                        TipoSolucao = "Factível";
                        MensagemSolucao = "Existem diversas formas de maratonar";
                        break;
                    case GLPKOutput.GLPKStatus.OptimalContinuousRelaxation:
                        TipoSolucao = "Ótima";
                        MensagemSolucao = "Existem diversas formas de maratonar";
                        break;
                    case GLPKOutput.GLPKStatus.Feasible:
                        TipoSolucao = "Factível";
                        MensagemSolucao = "Esta é uma boa combinação possível para maratonar";
                        break;
                    case GLPKOutput.GLPKStatus.ProbablyLocalOptimal:
                        TipoSolucao = "Ótima";
                        MensagemSolucao = "Esta é uma boa melhor combinação possível para maratonar";
                        break;
                    case GLPKOutput.GLPKStatus.LocalOptimal:
                        TipoSolucao = "Ótima";
                        MensagemSolucao = "Esta é uma boa combinação possível para maratonar";
                        break;
                    case GLPKOutput.GLPKStatus.Optimal:
                        TipoSolucao = "Ótima";
                        MensagemSolucao = "Esta é a melhor combinação possível para maratonar";
                        break;
                }
            }
            catch
            {
               
            }
        }
    }
}