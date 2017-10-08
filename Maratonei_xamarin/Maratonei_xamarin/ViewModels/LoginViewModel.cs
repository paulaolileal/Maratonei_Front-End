using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Data_Storage;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;

namespace Maratonei_xamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel, INotifyPropertyChanged
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

        public bool NetworkStatus
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }

        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public async Task<string> AutenticaUsuario(User p_User)
        {
            try
            {
                var v_Login = new UserLogin {nome = p_User.Nome, senha = p_User.Senha};
                string v_UserJson = JsonConvert.SerializeObject( v_Login );
                HttpClient v_HttpClient = new HttpClient();
                Uri v_RequestUri = new Uri(RequestURLs.LoginURL);
                HttpRequestMessage v_Request = new HttpRequestMessage()
                {
                    RequestUri = v_RequestUri,
                    Method = HttpMethod.Post,
                    Content = new StringContent(v_UserJson, Encoding.UTF8, "application/json")
                };
                HttpResponseMessage v_Response = await v_HttpClient.SendAsync(v_Request);
                string v_ReturnString = "";
                if (v_Response.IsSuccessStatusCode)
                {
                    v_ReturnString = await v_Response.Content.ReadAsStringAsync();

                }
                APIs.Instance.User = JsonConvert.DeserializeObject<User>(v_ReturnString);
                return v_ReturnString;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public void ArmazenaUsuarioLocal(User p_User)
        {
            var v_UserDAO = new UserDAO();
            v_UserDAO.InsertUser(p_User);
        }

        public bool CheckNetworkStatus()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
