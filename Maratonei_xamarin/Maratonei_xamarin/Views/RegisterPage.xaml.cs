using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterViewModel g_RegisterViewModel;
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = g_RegisterViewModel = new RegisterViewModel();
        }

        private void G_SubmitButton_OnClicked(object sender, EventArgs e)
        {
            
        }
    }
}