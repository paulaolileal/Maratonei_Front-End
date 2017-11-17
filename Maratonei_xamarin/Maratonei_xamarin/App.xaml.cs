using DLToolkit.Forms.Controls;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Services;
using Maratonei_xamarin.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation( XamlCompilationOptions.Compile )]
namespace Maratonei_xamarin {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            //FlowListView.Init();
            SetMainPage();
        }

        public static async void InitApis()
        {
            await APIs.Instance.Init();
        }

        public static void SetMainPage() {
            
            InitApis();

            //Current.MainPage = new MasterDetailPage1();
            //Current.MainPage = new NavigationPage( new SelecionarComidasPage(125));
            Current.MainPage = new NavigationPage(new LoginPage()) {BackgroundColor = Colors.NavbarColor};
            ////Current.MainPage = new MasterDetailPage1();
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
