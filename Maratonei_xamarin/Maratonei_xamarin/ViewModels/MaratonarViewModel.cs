using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.ViewModels;
using Maratonei_xamarin.Models;
using Newtonsoft.Json;
using TraktApiSharp.Objects.Get.Shows;

namespace Maratonei_xamarin.ViewModels {
    class MaratonarViewModel : BaseViewModel {
        public ItemMaratonarModel g_Model { get; set; }

        public MaratonarViewModel( List<TraktApiSharp.Objects.Get.Shows.TraktShow> p_listaSelecionados ) {
            g_Model = new ItemMaratonarModel();
            CarregarItems( p_listaSelecionados );
        }

        private void CarregarItems( List<TraktShow> p_listaSelecionados ) {
            g_Model.ListShow = new ObservableCollection<ItemMaratonarModel.ListaShowRequisicao>();
            foreach( var item in p_listaSelecionados ) {
                g_Model.ListShow.Add( new ItemMaratonarModel.ListaShowRequisicao { TraktShow = item } );
            }
        }

        public Requisicao CriarRequisicao() {
            var tempo = ( g_Model.HoraTotal * 60 ) + g_Model.MinutoTotal;
            // Criando Z
            // Cada série é uma variável
            var v_ob = new ObjectiveFunction();
            v_ob.Z = new List<Z>();
            v_ob.Type = ObjectiveFunction.FuncType.Max;
            v_ob.Solution = ObjectiveFunction.RespType.NotASolution;

            foreach( var listaShowRequisicao in g_Model.ListShow ) {
                v_ob.Z.Add( new Z { Item1 = listaShowRequisicao.TraktShow.Ids.Trakt.ToString(), Item2 = 1 } );
            }

            // Lita de Restrições
            List<Restriction> v_re = new List<Restriction>();

            // Tempo de cada episódio
            var v_listaTempos = new List<R>(
                from item in g_Model.ListShow
                select new R {
                    Item1 = item.TraktShow.Ids.Trakt.ToString(),
                    Item2 = item.TraktShow.Runtime ?? int.MaxValue
                }
            );
            v_listaTempos.Insert( 0, new R { Item1 = "", Item2 = tempo } );
            v_re.Add( new Restriction {
                Type = Restriction.FuncType.LessEqual,
                R = v_listaTempos
            } );

            // Pausas a cada episodio
            var v_listaPausas = new List<R>(
                    from item in g_Model.ListShow
                    select new R {
                        Item1 = item.TraktShow.Ids.Trakt.ToString(),
                        Item2 = item.TempoPausa
                    }
                );
            v_listaPausas.Insert( 0, new R { Item1 = "", Item2 = tempo } );
            v_re.Add( new Restriction {
                Type = Restriction.FuncType.LessEqual,
                R = v_listaPausas
            } );

            // Restrição de existencia
            for( var i = 0; i < g_Model.ListShow.Count; i++ ) {
                var v_listaExistencia = new List<R>();
                v_listaExistencia.Add( new R { Item1 = "", Item2 = 0 } );
                v_listaExistencia.AddRange( g_Model.ListShow.Select( ( t, j ) => new R {
                    Item1 = t.TraktShow.Ids.Trakt.ToString(),
                    Item2 = i == j ? 1 : 0
                } ) );
                v_re.Add( new Restriction {
                    Type = Restriction.FuncType.GreaterEqual,
                    R = v_listaExistencia
                } );
            }

            // Quant mínima de epi(s)
            for( var i = 0; i < g_Model.ListShow.Count; i++ ) {
                var v_listaQuantMinEpis = new List<R>();
                v_listaQuantMinEpis.Add( new R { Item1 = "", Item2 = g_Model.ListShow[i].MinimoEpisodios } );
                v_listaQuantMinEpis.AddRange( g_Model.ListShow.Select( ( t, j ) => new R {
                    Item1 = t.TraktShow.Ids.Trakt.ToString(),
                    Item2 = i == j ? 1 : 0
                } ) );
                v_re.Add( new Restriction {
                    Type = Restriction.FuncType.GreaterEqual,
                    R = v_listaQuantMinEpis
                } );
            }

            //Quant máxima de epi(s)
            for( var i = 0; i < g_Model.ListShow.Count; i++ ) {
                var v_listaQuantMaxEpis = new List<R>();
                v_listaQuantMaxEpis.Add( new R { Item1 = "", Item2 = g_Model.ListShow[i].TraktShow.AiredEpisodes ?? 0 } );
                v_listaQuantMaxEpis.AddRange( g_Model.ListShow.Select( ( t, j ) => new R {
                    Item1 = t.TraktShow.Ids.Trakt.ToString(),
                    Item2 = i == j ? 1 : 0
                } ) );
                v_re.Add( new Restriction {
                    Type = Restriction.FuncType.LessEqual,
                    R = v_listaQuantMaxEpis
                } );
            }

            return new Requisicao { ObjectiveFunction = v_ob, Restrictions = v_re };
        }

        public async Task Maratonar() {
            var v_req = CriarRequisicao();
            var v_JReq = JsonConvert.SerializeObject( v_req );
            var v_HttpClient = new HttpClient();
            var v_Request = new HttpRequestMessage() {
                RequestUri = new Uri( RequestURLs.SimplexURL ),
                Method = HttpMethod.Post,
                Content = new StringContent( v_JReq, Encoding.UTF8, "application/json" )
            };
            var v_Response = await v_HttpClient.SendAsync( v_Request );
            string v_stringResp = "";
            if( v_Response.IsSuccessStatusCode ) {
                v_stringResp = await v_Response.Content.ReadAsStringAsync();
            }
            ObjectiveFunction v_solucao = JsonConvert.DeserializeObject<ObjectiveFunction>( v_stringResp );
        }
    }
}
