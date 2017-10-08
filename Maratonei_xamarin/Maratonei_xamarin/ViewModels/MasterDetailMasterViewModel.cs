using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.Services;
using Maratonei_xamarin.Views;
using TraktApiSharp.Objects.Get.Users;
using TraktApiSharp.Requests.Params;

namespace Maratonei_xamarin.ViewModels {
    class MasterDetailMasterViewModel : BaseViewModel {
        private TraktUser _traktUser;
        private User _user;
        public ObservableCollection<MasterDetailPage1MenuItem> MenuItems { get; set; }

        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public TraktUser TraktUser
        {
            get => _traktUser;
            set => SetProperty(ref _traktUser, value);
        }

        public MasterDetailMasterViewModel()
        {
            MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>( new[]
            {
                new MasterDetailPage1MenuItem { Id = 0, Title = "Home" },
                new MasterDetailPage1MenuItem { Id = 1, Title = "Watchlist", TargetType = typeof(WhatchList)},
                new MasterDetailPage1MenuItem { Id = 2, Title = "Pesquisa Séries", TargetType = typeof(SearchShows) },
                new MasterDetailPage1MenuItem { Id = 3, Title = "About", TargetType = typeof(AboutPage) },

                //new MasterDetailPage1MenuItem { Id = 0, Title = "Login", TargetType = typeof(LoginPage)},
                //new MasterDetailPage1MenuItem { Id = 2, Title = "Teste", TargetType = typeof(ItemsPage) },
                //new MasterDetailPage1MenuItem { Id = 5, Title = "Selecionar Episodios", TargetType = typeof(SelecionarTemporadas)},
                //new MasterDetailPage1MenuItem { Id = 5, Title = "Maratonar", TargetType = typeof(MaratonarPage)},
            } );
            CarregarUsuario();
        }

        private async void CarregarUsuario()
        {
            User = APIs.Instance.User;
            TraktUser = await APIs.Instance.MainTraktClient.Users.GetUserProfileAsync(User.TraktUser,new TraktExtendedInfo{Full = true});
        }
    }
}
