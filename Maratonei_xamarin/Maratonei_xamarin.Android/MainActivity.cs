using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;

namespace Maratonei_xamarin.Droid {
    [Activity( Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate( Bundle bundle ) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            UserDialogs.Init(() => (Activity) Forms.Context);

            base.OnCreate( bundle );

            global::Xamarin.Forms.Forms.Init( this, bundle );

            LoadApplication( new App() );
        }
    }
}