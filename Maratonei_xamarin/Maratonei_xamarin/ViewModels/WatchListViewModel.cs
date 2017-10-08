using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Models;
using Maratonei_xamarin.Services;
using TraktApiSharp.Enums;
using TraktApiSharp.Objects.Get.Shows;
using TraktApiSharp.Requests.Params;

namespace Maratonei_xamarin.ViewModels {
    public class WatchListViewModel : BaseViewModel {
        public ObservableCollection<ItemSearchShow> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        public bool NadaEncontrado {
            get => g_NadaEncontrado;
            set => SetProperty( ref g_NadaEncontrado, value );
        }

        public List<TraktShow> g_listaSelecionados = new List<TraktShow>();
        private ObservableCollection<ItemSearchShow> _items;
        private bool g_NadaEncontrado;


        public WatchListViewModel() {
            CarregarWatchList();
        }

        private async void CarregarWatchList()
        {
            NadaEncontrado = false;
            IsBusy = true;
            var user = APIs.Instance.User.TraktUser;
            var lista = await APIs.Instance.MainTraktClient.Users.GetWatchlistAsync( user,
                TraktSyncItemType.Show, new TraktExtendedInfo { Full = true } );
            Items = new ObservableCollection<ItemSearchShow>( lista.Select( a => new ItemSearchShow { TraktSearchResult = a.Show } ) );
            NadaEncontrado = !Items.Any();
            IsBusy = false;
        }
    }
}
