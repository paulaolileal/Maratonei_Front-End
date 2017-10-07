using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Maratonei_xamarin.Services;
using TraktApiSharp.Objects.Get.Shows;

namespace Maratonei_xamarin.Models {
    class SolucaoModel : BaseDataObject
    {
        private double _solucao;
        public double Solucao
        {
            get => Math.Round( _solucao, 2 );
            set => SetProperty(ref _solucao, value);
        }

        private TraktShow _show;
        public TraktShow Show
        {
            get => _show;
            set => SetProperty(ref _show, value);
        }

        private string _showImage;
        public string ShowImage {
            get => _showImage;
            set => SetProperty( ref _showImage, value );
        }

        public SolucaoModel(double solucao, TraktShow show)
        {
            _solucao = solucao;
            _show = show;
            try
            {
                AtualizarImagem();
            }
            catch (Exception ex){ Debug.WriteLine(ex.StackTrace); }
        }

        public async Task AtualizarImagem() {
            ShowImage = await APIs.Instance.PegarImagem( Show.Ids.Tvdb );
        }

    }
}
