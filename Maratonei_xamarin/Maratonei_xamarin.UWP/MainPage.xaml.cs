using ImageCircle.Forms.Plugin.UWP;

namespace Maratonei_xamarin.UWP {
    public sealed partial class MainPage {
        public MainPage() {
            this.InitializeComponent();
            LoadApplication( new Maratonei_xamarin.App() );
        }
    }
}