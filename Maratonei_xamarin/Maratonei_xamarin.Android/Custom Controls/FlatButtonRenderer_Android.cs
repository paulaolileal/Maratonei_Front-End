using Android.Graphics;
using Java.Lang;
using Maratonei_xamarin.Custom_Controls;
using Maratonei_xamarin.Droid.Custom_Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FlatButton), typeof(FlatButtonRenderer_Android))]
namespace Maratonei_xamarin.Droid.Custom_Controls
{
    public class FlatButtonRenderer_Android : ButtonRenderer
    {
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
        }
    }
}