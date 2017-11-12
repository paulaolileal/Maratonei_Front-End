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
    public class SelecionarComidasViewModel: BaseViewModel
    {
        public ObservableCollection<Comida> Comidas { get; set; }
        private Double maratona;
        public double Maratona { get => maratona; set => SetProperty( ref maratona, value); }


        public SelecionarComidasViewModel()
        {
            Comidas = new ObservableCollection<Comida>();
            Maratona = 40;
        }

        public void AddComida(string nome, string quant)
        {
            Comidas.Add(new Comida { Nome = nome, Quantidade = Int32.Parse(quant) });
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

        public void EnviarRequisicao()
        {
            Lanchar();
        }

        private GLPKInput CriarRequisicao()
        {
            GLPKInput inp = new GLPKInput();
            foreach (var item in Comidas)
            {

            }
            return inp;
        }
    }
}
