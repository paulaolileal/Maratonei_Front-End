using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraktApiSharp.Objects.Get.Shows.Episodes;

namespace Maratonei_xamarin.Models {
    public class SeasonModel : ObservableCollection<TraktApiSharp.Objects.Get.Shows.Episodes.TraktEpisode>{
        public SeasonModel( IEnumerable<TraktEpisode> collection ) : base( collection ) {
        }

        public TraktApiSharp.Objects.Get.Shows.Seasons.TraktSeason Season { get; set; }
    }
}
