using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Services;
using TraktApiSharp.Objects.Get.Shows;

namespace Maratonei_xamarin.Models {
    public class ItemMaratonarModel : BaseDataObject {
        public class ListaShowRequisicao : BaseDataObject {
            private TraktShow _traktShow;
            public TraktShow TraktShow
            {
                get => _traktShow;
                set
                {
                    SetProperty(ref _traktShow, value);
                    AtualizarImagem();
                }
            }

            private int _tempoPausa;
            public int TempoPausa {
                get => _tempoPausa;
                set => SetProperty( ref _tempoPausa, value );
            }

            private List<ItemSelecionarTemporada> _listaTemporadas;
            public List<ItemSelecionarTemporada> ListaTemporadas {
                get => _listaTemporadas;
                set {
                    SetProperty( ref _listaTemporadas, value );
                    AtualizarTotalEpisodios();
                }
            }

            private int _episodiosSelecionados;

            public int EpisodiosSelecionados {
                get => _episodiosSelecionados;
                set => SetProperty( ref _episodiosSelecionados, value );
            }

            private int _minimoEpisodios;
            public int MinimoEpisodios
            {
                get => _minimoEpisodios;
                set => SetProperty(ref _minimoEpisodios, value);
            }

            private string showImage;
            public string ShowImage
            {
                get => showImage;
                set => SetProperty(ref showImage, value);
            }

            public void AtualizarTotalEpisodios() {
                int valor = 0;
                foreach( var itemSelecionarTemporada in _listaTemporadas ) {
                    if( itemSelecionarTemporada.Selecionado ) {
                        valor += itemSelecionarTemporada.Season.TotalEpisodesCount ?? 0;
                    }
                    else {
                        valor += itemSelecionarTemporada.EpisodiosSelecionados;
                    }
                }
                this.EpisodiosSelecionados = valor;
            }

            public async Task AtualizarImagem()
            {
                ShowImage = await APIs.Instance.PegarImagem(TraktShow.Ids.Tvdb);
            }

        }


        private ObservableCollection<ListaShowRequisicao> _listShow;

        public ObservableCollection<ListaShowRequisicao> ListShow {
            get => _listShow;
            set => SetProperty( ref _listShow, value );
        }

        private int _horaTotal;
        public int HoraTotal {
            get => _horaTotal;
            set => SetProperty( ref _horaTotal, value );
        }

        private int _minutoTotal;
        public int MinutoTotal {
            get => _minutoTotal;
            set => SetProperty( ref _minutoTotal, value );
        }

    }
}
