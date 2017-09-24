using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraktApiSharp;
using TvDbSharper;

namespace Maratonei_xamarin.Services {
    class APIs {
        public TraktClient MainTraktClient { get; set; }
        public TvDbClient MainTvDbClient { get; set; }

        private APIs() {
        }
        private static APIs instance;
        public static APIs Instance => instance ?? (instance = new APIs());

        public async void Init()
        {
            MainTraktClient = new TraktClient(
                "291a8dd6ebf31265856c34b0fc6e9be0f81269e82de0f267e654bcfc6bf2a857",
                "a4f0a9ebf050c37f95a3ee18fffc96aa33d5b188e6c526f81a5a1c2572384ff1"
            );
            MainTvDbClient = new TvDbClient { AcceptedLanguage = "en" };
            await MainTvDbClient.Authentication.AuthenticateAsync( "FEF236EED282A656" );
        }
    }
}
