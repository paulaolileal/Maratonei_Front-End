using Maratonei_xamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maratonei_xamarin.ViewModels {

    class SolucaoViewModel : BaseViewModel {
        public ObservableCollection<SolucaoModel> g_SolucaoList { get; set; }

        public string TipoSolucao {
            get { return tipoSolucao; }
            set { SetProperty( ref tipoSolucao, value ); }
        }

        public string MensagemSolucao {
            get { return mensagemSolucao; }
            set { SetProperty( ref mensagemSolucao, value ); }
        }

        public string Sugestao {
            get { return sugestao; }
            set { SetProperty( ref sugestao, value ); }
        }

        public string Total {
            get { return _total; }
            set { SetProperty( ref _total, value ); }
        }

        private string mensagemSolucao;
        private string tipoSolucao;
        private string sugestao;
        private string _total;

        public SolucaoViewModel( ItemMaratonarModel p_Maratona, ObjectiveFunction p_Solucao ) {
            g_SolucaoList = new ObservableCollection<SolucaoModel>();

            switch( p_Solucao.Solution ) {
                case ObjectiveFunction.RespType.Optimum:
                    TipoSolucao = "Ótima";
                    mensagemSolucao = "Esta é a melhor combinação possível para maratonar";
                    Sugestao +=( "Os resultados apresentam a quantidade de episódios à assistir" ) + "\n";
                    break;
                case ObjectiveFunction.RespType.Unlimited:
                    TipoSolucao = "Ilimitada";
                    mensagemSolucao = "Com os dados informados não é possível atingir uma solução completa";
                    Sugestao +=( "Estem é um resultado parcial" ) + "\n";
                    Sugestao +=( "Os resultados apresentam a quantidade de episódios à assistir" ) + "\n";
                    break;
                case ObjectiveFunction.RespType.Multiple:
                    TipoSolucao = "Múltipla";
                    mensagemSolucao = "Existem diversas formas de maratonar";
                    Sugestao +=( "Os resultados apresentam uma destas formas" ) + "\n";
                    Sugestao +=( "Os resultados apresentam a quantidade de episódios à assistir" ) + "\n";
                    break;
                case ObjectiveFunction.RespType.Impossible:
                    TipoSolucao = "Impossível";
                    mensagemSolucao = "Não existe combinações possíveis para maratonar";
                    Sugestao +=( "Verifique se o tempo disponível foi preenchido" ) + "\n";
                    Sugestao +=( "Verifique a quantidade mínima escolhida escolhida de episódios" ) + "\n";
                    Sugestao +=( "Verifique a de episódios selecionados" ) + "\n";
                    break;
                case ObjectiveFunction.RespType.NotASolution:
                    TipoSolucao = "Sem Solução";
                    mensagemSolucao = "Algo deu errado ao calcular o resultado";
                    Sugestao +=( "Verifique os dados informados" );
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Total = Math.Round(p_Solucao.Z.FirstOrDefault( a => a.Item1.Equals( "Z" ) ).Item2, 2).ToString();

            foreach( var listaShowRequisicao in p_Maratona.ListShow ) {
                var t = p_Solucao.Z.Find( a => a.Item1.Equals( listaShowRequisicao.TraktShow.Ids.Trakt.ToString() ) );
                g_SolucaoList.Add(
                    new SolucaoModel(
                        t.Item2,
                       listaShowRequisicao.TraktShow
                    )
               );
            }

        }
    }
}

