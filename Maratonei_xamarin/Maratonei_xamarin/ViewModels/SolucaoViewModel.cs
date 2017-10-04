using Maratonei_xamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maratonei_xamarin.ViewModels {

    class SolucaoViewModel {
        public ObservableCollection<SolucaoModel> g_SolucaoList { get; set; }

        public SolucaoViewModel( ItemMaratonarModel p_Maratona, ObjectiveFunction p_Solucao ) {
            g_SolucaoList = new ObservableCollection<SolucaoModel>();

            foreach( var listaShowRequisicao in p_Maratona.ListShow )
            {
                var t = p_Solucao.Z.Find(a => a.Item1.Equals(listaShowRequisicao.TraktShow.Ids.Trakt.ToString()));
                g_SolucaoList.Add(
                    new SolucaoModel (
                        t.Item2,
                       listaShowRequisicao.TraktShow
                    )
               );
            }

        }

    }
}

