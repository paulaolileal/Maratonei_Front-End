using Maratonei_xamarin.Custom_Controls;
using Maratonei_xamarin.UWP.Custom_Controls;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(FlatButton),typeof(FlatButtonRenderer_UWP))]
namespace Maratonei_xamarin.UWP.Custom_Controls
{
    public class FlatButtonRenderer_UWP : ButtonRenderer
    {
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}