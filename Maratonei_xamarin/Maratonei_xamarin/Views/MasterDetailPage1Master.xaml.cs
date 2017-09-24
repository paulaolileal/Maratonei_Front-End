﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class MasterDetailPage1Master : ContentPage {
        public ListView ListView;

        public MasterDetailPage1Master() {
            InitializeComponent();

            BindingContext = new MasterDetailPage1MasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailPage1MasterViewModel : INotifyPropertyChanged {
            public ObservableCollection<MasterDetailPage1MenuItem> MenuItems { get; set; }

            public MasterDetailPage1MasterViewModel() {

                MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>( new[]
                {
                    new MasterDetailPage1MenuItem { Id = 0, Title = "Login", TargetType = typeof(LoginPage)},
                    new MasterDetailPage1MenuItem { Id = 1, Title = "About", TargetType = typeof(AboutPage) },
                    new MasterDetailPage1MenuItem { Id = 2, Title = "Teste", TargetType = typeof(ItemsPage) },
                    new MasterDetailPage1MenuItem { Id = 3, Title = "Pesquisa Séries", TargetType = typeof(SearchShows) },
                    new MasterDetailPage1MenuItem { Id = 4, Title = "Outro Teste" },
                    new MasterDetailPage1MenuItem { Id = 5, Title = "Selecionar Episodios", TargetType = typeof(SelecionarEpisodios)},
                } );
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged( [CallerMemberName] string propertyName = "" ) {
                if( PropertyChanged == null )
                    return;

                PropertyChanged.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
            }
            #endregion
        }
    }
}