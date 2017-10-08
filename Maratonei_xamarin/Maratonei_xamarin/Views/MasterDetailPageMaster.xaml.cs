using System;
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

            ListView = MenuItemsListView;
        }
        
    }
}