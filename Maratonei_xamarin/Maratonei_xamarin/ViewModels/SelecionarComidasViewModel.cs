using Maratonei.Models;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Maratonei_xamarin.ViewModels
{
    public class SelecionarComidasViewModel : BaseViewModel
    {
        public ObservableCollection<Comida> Comidas { get; set; }
        public ObservableCollection<ComidaSerie> SelecionarComidas { get; set; }
        ObservableCollection<SolucaoModel> g_SolucaoList;
        private double maratona;
        public double Maratona { get => maratona; set => SetProperty(ref maratona, value); }
        public ObservableCollection<SolucaoModel> G_SolucaoList { get => g_SolucaoList; set => g_SolucaoList = value; }



        public SelecionarComidasViewModel(ObservableCollection<SolucaoModel> sol)
        {
            Comidas = new ObservableCollection<Comida>();
            SelecionarComidas = new ObservableCollection<ComidaSerie>();
            G_SolucaoList = sol;
            foreach (var item in G_SolucaoList)
            {
                SelecionarComidas.Add(new ComidaSerie { Serie = item.Show.Title });
            }
        }

        public void AddComida(string nome, string min)
        {
            var j = JsonConvert.SerializeObject(SelecionarComidas.ToList());
            var l = JsonConvert.DeserializeObject<List<ComidaSerie>>(j);
            Comidas.Add(new Comida { Nome = nome,
                MaxPorEpi = int.Parse(min),
                ComidaSeries = l
            });
        }



        internal void RemoverComida(Comida co)
        {
            Comidas.Remove(co);
        }

        public async Task<GLPKOutput> Lanchar()
        {
            IsBusy = true;
            
                var v_req = CriarRequisicao();
                var v_JReq = JsonConvert.SerializeObject(v_req);
                var v_HttpClient = new HttpClient();
                var v_Request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(RequestURLs.GLPKURL),
                    Method = HttpMethod.Post,
                    Content = new StringContent(v_JReq, Encoding.UTF8, "application/json")
                };
                var v_Response = await v_HttpClient.SendAsync(v_Request);
                string v_stringResp = "";
                if (v_Response.IsSuccessStatusCode)
                {
                    v_stringResp = await v_Response.Content.ReadAsStringAsync();
                }
            
            
            IsBusy = false;
            return JsonConvert.DeserializeObject<GLPKOutput>(v_stringResp);
        }

        private GLPKInput CriarRequisicao()
        {
            GLPKInput inp = new GLPKInput();

            //Objetivo
            // FO -> max(z) = x1 + x2 + x3 + x4 + …. + xn
            // Todos terão valor 1
            inp.Objective = new GLPKObjective(Enumerable.Repeat(1.0, Comidas.Count).ToList()) { Operation = GLPKObjective.Operator.Maxmize };
            //variaveis
            inp.Variables = Comidas.Select(a => a.Nome).ToList();

            // R1 = a1.x1 + b1.x2 + c1.x3 + … + n1.xn <= numEpiSerie1
            //R2 = a2.x1 + b2.x2 + c2.x3 + … + n2.xn <= numEpiSerie2
            //...
            //RM = am.x1 + bm.x2 + cm.x3 + … + nm.xn <= numEpiSerieM
            // G_SolucaoList == séries maratonadas
            foreach(var item in G_SolucaoList)
            {
                var lista = new List<double>();
                lista.AddRange(Comidas.Select((t, j) => t.ComidaSeries.First(a => a.Serie.Equals(item.Show.Title)).Quantidade) );
                // Disponibilidade será número de episódios por série
                inp.Restrictions.Add(new GLPKRestriction
                {
                    Disponibility = item.Solucao,
                    Values = lista,
                    Operation = GLPKRestriction.Operator.LessOrEqual
                });
            }

            // R0 = 0 <= x1;x2;x3 … xn <= maxDeLanche1 … maxDeLancheM
            for (int i = 0; i < Comidas.Count; i++)
            {
                if (Comidas[i].MaxPorEpi <= 0) continue;
                var lista = new List<double>();
                // Variável atual terá valor 1 e demais 0 
                lista.AddRange(Comidas.Select((t, j) => i == j ? 1.0 : 0.0));
                // Disponibilidade será a quantidade máxima consumida por episódio
                inp.Restrictions.Add(new GLPKRestriction
                {
                    Disponibility = Comidas[i].MaxPorEpi,
                    Values = lista,
                    Operation = GLPKRestriction.Operator.LessOrEqual
                });
            }

            return inp;
        }
    }
}
