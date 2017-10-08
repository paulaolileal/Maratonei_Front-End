using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Helpers;
using Maratonei_xamarin.Models;
using TraktApiSharp;
using TraktApiSharp.Objects.Get.Users;
using TvDbSharper;

namespace Maratonei_xamarin.Services {
    class APIs
    {
        private User user;
        public TraktClient MainTraktClient { get; set; }
        public TvDbClient MainTvDbClient { get; set; }
        private static APIs instance;
        public static APIs Instance => instance ?? (instance = new APIs());

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        private APIs() { }

        public Task Init()
        {
            MainTraktClient = new TraktClient(
                "291a8dd6ebf31265856c34b0fc6e9be0f81269e82de0f267e654bcfc6bf2a857",
                "a4f0a9ebf050c37f95a3ee18fffc96aa33d5b188e6c526f81a5a1c2572384ff1"
            );
            MainTvDbClient = new TvDbClient { AcceptedLanguage = "en" };
            try
            {
                return MainTvDbClient.Authentication.AuthenticateAsync("FEF236EED282A656");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                return MainTvDbClient.Authentication.RefreshTokenAsync();
            }
        }

        public async Task<string> PegarImagem(uint? id)
        {
            try
            {
                var i = await MainTvDbClient.Series.GetImagesAsync(
                    (int)id,
                    new TvDbSharper.Dto.ImagesQuery() {KeyType = TvDbSharper.Dto.KeyType.Poster}
                );
                return i.Data.Length > 0 ? (i.Data[i.Data.Length - 1].getImageUrl()) : "no_image.png";
            }
            catch
            {
                return "no_image.png";
            }
            
        }
    }
}
