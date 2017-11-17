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
            inp.Objective = new GLPKObjective(Enumerable.Repeat(1.0, Comidas.Count).ToList()) { Operation = GLPKObjective.Operator.Maxmize };

            //variaveis
            inp.Variables = Comidas.Select(a => a.Nome).ToList();

            foreach(var item in G_SolucaoList)
            {
                var lista = new List<double>();
                lista.AddRange(Comidas.Select((t, j) => t.ComidaSeries.First(a => a.Serie.Equals(item.Show.Title)).Quantidade) );

                inp.Restrictions.Add(new GLPKRestriction
                {
                    Disponibility = item.Solucao,
                    Values = lista,
                    Operation = GLPKRestriction.Operator.LessOrEqual
                });
            }

            for (int i = 0; i < Comidas.Count; i++)
            {
                if (Comidas[i].MaxPorEpi <= 0) continue;
                var lista = new List<double>();
                lista.AddRange(Comidas.Select((t, j) => i == j ? 1.0 : 0.0));
                inp.Restrictions.Add(new GLPKRestriction
                {
                    Disponibility = Comidas[i].MaxPorEpi,
                    Values = lista,
                    Operation = GLPKRestriction.Operator.LessOrEqual
                });
            }

            ////R1 = lancheX + lancheY + lancheW + lancheZ <= quantLanchesTotal
            //var quantTotal = 0.0;
            //foreach (var item in Comidas)
            //{
            //    quantTotal += item.Quantidade;
            //}
            //inp.Restrictions.Add(new GLPKRestriction {
            //    Disponibility = quantTotal,
            //    Values = Comidas.Select(a => a.Quantidade).ToList(),
            //    Operation = GLPKRestriction.Operator.LessOrEqual
            //});

            ////R2 = lancheX + lancheY + lancheW + lancheZ >= minDeLanchePorEpisodio
            //for (int i = 0; i < Comidas.Count; i++)
            //{
            //    var lista = new List<double>();
            //    lista.AddRange(Comidas.Select((t, j) => i == j ? 1.0 : 0.0));
            //    inp.Restrictions.Add(new GLPKRestriction
            //    {
            //        Disponibility = Comidas[i].Min,
            //        Values = lista,
            //        Operation = GLPKRestriction.Operator.GreaterOrEqual
            //    });
            //}

            ////R3 = lancheX + lancheY + lancheW + lancheZ >= numEpis maratonados
            //inp.Restrictions.Add(new GLPKRestriction
            //{
            //    Disponibility = Maratona,
            //    Values = Comidas.Select(a => a.Quantidade).ToList(),
            //    Operation = GLPKRestriction.Operator.GreaterOrEqual
            //});

            //R4 = x; y; w; z => 0
            //for (int i = 0; i < Comidas.Count; i++)
            //{
            //    var lista = new List<double>();
            //    lista.AddRange(Comidas.Select((t, j) => i == j ? 1.0 : 0.0 ));
            //    inp.Restrictions.Add(new GLPKRestriction
            //    {
            //        Disponibility = 0,
            //        Values = lista,
            //        Operation = GLPKRestriction.Operator.GreaterOrEqual
            //    });
            //}


            return inp;
        }
    }
}
