using Maratonei.Models;
using Maratonei_xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratonei_xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SolucaoLancharPage : ContentPage
    {
        public SolucaoLancharViewModel ViewModel;
        public SolucaoLancharPage(GLPKOutput resp)
        {
            InitializeComponent();
            BindingContext = ViewModel = new SolucaoLancharViewModel(resp);
        }
    }
}