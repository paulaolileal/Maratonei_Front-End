using DLToolkit.Forms.Controls;
using Maratonei_xamarin.Services;
using Maratonei_xamarin.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation( XamlCompilationOptions.Compile )]
namespace Maratonei_xamarin {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            FlowListView.Init();
            SetMainPage();
            APIs.Instance.Init();
        }

        public static void SetMainPage() {
            Current.MainPage = new MasterDetailPage1();
            //    new TabbedPage {
            //    Children =
            //    {
            //        new NavigationPage(new ItemsPage())
            //        {
            //            Title = "Browse",
            //            Icon = Device.OnPlatform("tab_feed.png",null,null)
            //        },
            //        new NavigationPage(new AboutPage())
            //        {
            //            Title = "About",
            //            Icon = Device.OnPlatform("tab_about.png",null,null)
            //        },
            //    }
            //};
        }
    }
}
