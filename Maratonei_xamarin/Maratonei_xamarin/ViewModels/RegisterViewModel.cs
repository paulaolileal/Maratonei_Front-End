using System;
using System.ComponentModel;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Data_Storage;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TraktApiSharp;
using TraktApiSharp.Modules;
using TraktApiSharp.Objects.Get.Users;

namespace Maratonei_xamarin.ViewModels
{
    public class RegisterViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string CLIENT_ID = "8b52de13c749647158ee90572d975e5b3a12af509a67a6448b89e1b3095a5081";
        private string CLIENT_SECRET = "f9ab8994b6fe8c52717173516a4962830d274f3e84cbae8093b1d4022c7b0027";

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

        public async Task<bool> ValidaUsuarioTrakt(string p_UserTrakt)
        {
            var v_Result = true;
            var v_TraktClient = new TraktClient(CLIENT_ID,CLIENT_SECRET);
            TraktUser v_UserProfile;
            
            try
            {
                v_UserProfile = await v_TraktClient.Users.GetUserProfileAsync(p_UserTrakt);
                int x;
            }
            catch (Exception e)
            {

                v_Result = false;
            }
            return v_Result;
        }

        public async Task<string> CadastraNovoUsuario(User p_User)
        {
            var v_ResultCadastro = "";
            var v_HttpClient = new HttpClient();
            dynamic v_UserJsonObj = new ExpandoObject();

            v_UserJsonObj.nome = p_User.Nome;
            v_UserJsonObj.senha = p_User.Senha;
            v_UserJsonObj.traktUser = p_User.TraktUser;

            var v_UserJsonString = JsonConvert.SerializeObject(v_UserJsonObj);
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