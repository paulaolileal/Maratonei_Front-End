using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Data_Storage;
using Maratonei_xamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class MasterDetailPage1 : MasterDetailPage {
        private User v_User;

        public MasterDetailPage1() {
            InitializeComponent();
            if( Device.RuntimePlatform == Device.UWP ) {
                MasterBehavior = MasterBehavior.Popover; // Added this line of code
            }
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        public MasterDetailPage1(User v_User)
        {
            NavigationPage.SetHasNavigationBar(this,false);
            InitializeComponent();
            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover; // Added this line of code
            }
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            this.v_User = v_User;
        }

        private void ListView_ItemSelected( object sender, SelectedItemChangedEventArgs e ) {
            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            if( item == null )
                return;

            var page = (ContentPage) Activator.CreateInstance( item.TargetType );
            page.Title = item.Title;

            Detail = new NavigationPage( page );
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}