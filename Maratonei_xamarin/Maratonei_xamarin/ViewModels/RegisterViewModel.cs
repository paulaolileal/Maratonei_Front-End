using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Data_Storage;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Models;
using Newtonsoft.Json.Linq;

namespace Maratonei_xamarin.ViewModels
{
    public class RegisterViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }
        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public async Task<string> CadastraNovoUsuario(User p_User)
        {
            var v_ResultCadastro = "";
            var v_HttpClient = new HttpClient();
            dynamic v_UserJsonObj = new JObject();

            v_UserJsonObj.nome = p_User.Nome;
            v_UserJsonObj.senha = p_User.Senha;
            v_UserJsonObj.traktUser = p_User.TraktUser;

            var v_UserJsonString = v_UserJsonObj.ToString();
            var v_RequestUri = new Uri(RequestURLs.RegisterUserURL);

            try
            {
                var v_HttpRequest = new HttpRequestMessage()
                {
                    RequestUri = v_RequestUri,
                    Method = HttpMethod.Post,
                    Content = new StringContent(v_UserJsonString, Encoding.UTF8, "application/json")
                };

                var v_Response = await v_HttpClient.SendAsync(v_HttpRequest);
                if (v_Response.IsSuccessStatusCode)
                {
                    v_ResultCadastro = await v_Response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return v_ResultCadastro;
        }

        public void InsereNovoUsuario(User p_User)
        {
            var v_UserDAO = new UserDAO();
            v_UserDAO.InsertUser(p_User);
        }
    }
}